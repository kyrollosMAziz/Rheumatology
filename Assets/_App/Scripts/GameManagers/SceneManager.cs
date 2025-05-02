using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : GenericSingleton<SceneManager>
{
    // public List<SerializedPatientPosition> SerializedPatientPositions;

    public Queue<GameSequence> GameEventsQueue;
    public List<GameSequence> GameEvents;
    public List<IQuestionAnswered> QuestionsAnswered;
    private bool isCursorActive;

    public bool IsCursorActive
    {
        set
        {
            isCursorActive = value;
            if (!isCursorActive)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public GameSequencePhase currentPhase;

    private void Awake()
    {
        IsCursorActive = false;
        GameEventsQueue = new Queue<GameSequence>(GameEvents);
    }

    private void Start()
    {
        DialoguesManager.Instance.currentSequence = PopGameSequence();
        DialoguesManager.Instance.StartDialogueSequenceHandler(3);
        UIManager.Instance.HideEndGameCanvas();
    }

    #region test

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.L))
            WaypointManager.Instance.InitiateWaypointMovement(0, PlayerManager.Instance.CameraContainer.transform);
#endif
    }

    #endregion

    public void LoadNextSequence(GameSequencePhase phase)
    {
        print("SceneManager" + phase);
        CameraControl.Instance.LockCamera();

        WaypointManager.Instance.InitiateWaypointMovement(phase,
            PlayerManager.Instance.CameraContainer.transform,
            () =>
            {
                SceneManager.Instance.SetPlayerPateintTarget(phase);
                DialoguesManager.Instance.StartDialogueSequenceHandler(2);
                CameraControl.Instance.UnlockCamera();
            });
        //QuestionsManager.Instance.HideQuestion();
    }

    private void SetPlayerPateintTarget(GameSequencePhase gameSequencePhase)
    {
        currentPhase = gameSequencePhase;
    }

    public GameSequencePhase GetCurrentPhase()
    {
        return currentPhase;
    }


    public GameSequence PopGameSequence()
    {
        if (GameEventsQueue.Count == 0)
        {
            return null;
        }

        return GameEventsQueue.Dequeue();
    }

    public void EndGame()
    {
        UIManager.Instance.ShowEndGameCanvas();
        CameraControl.Instance.LockCamera();
        IsCursorActive = true;
    }

    #region ObserverPattern

    //public void AssignQuestionAnsweredListeners(IQuestionAnswered questionAnsweredListeners)
    //{
    //    QuestionsAnswered.Add(questionAnsweredListeners);
    //}
    //public void InvokeQuestionListeners()
    //{
    //    for (int i = 0; i < QuestionsAnswered.Count; i++) 
    //    {
    //        QuestionsAnswered[i].OnQuestionAnswered();
    //    }
    //}
    //public void RemoveQuestionAnsweredListeners() 
    //{
    //    QuestionsAnswered.Clear();
    //}

    #endregion
}