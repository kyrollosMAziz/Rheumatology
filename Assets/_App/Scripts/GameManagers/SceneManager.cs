using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : GenericSingleton<SceneManager>
{
    public Queue<GameSequence> GameEventsQueue;
    public List<GameSequence> GameEvents;
    public List<IQuestionAnswered> QuestionsAnswered;
    public enum GameSequencePhase
    {
        phase0 = 0, phase1 = 1, phase2 = 2,
        phase3 = 3, phase4 = 4, phase5 = 5, 
        phase6 = 6, phase7 = 7, phase8 = 8,
        phase9 = 9
    };

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

    private void Awake()
    {
        IsCursorActive = false;
        GameEventsQueue = new Queue<GameSequence>(GameEvents);
    }
    private void Start()
    {
        DialoguesManager.Instance.currentSequence = PopGameSequence();
        DialoguesManager.Instance.StartDialogueSequenceHandler(2);
        UIManager.Instance.HideEndGameCanvas();
    }
    public void LoadSequence()
    {
        //DialoguesManager.Instance.StartDialogueSequenceHandler(2);
        //PlayerManager.Move
        QuestionsManager.Instance.HideQuestion();
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
