using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : GenericSingleton<SceneManager>
{
    public Stack<GameSequence> GameEventsStack;
    public List<GameSequence> GameEvents;
    public List<IQuestionAnswered> QuestionsAnswered;

    private void Awake()
    {
        GameEventsStack = new Stack<GameSequence>(GameEvents);
    }
    private void Start()
    {
        DialoguesManager.Instance.currentSequence = PopGameSequence();
        DialoguesManager.Instance.StartDialogueSequenceHandler();
    }
    public GameSequence PopGameSequence() 
    {
        if (GameEventsStack.Count == 0) 
        {
            EndGame();
            return null;
        }
        return GameEventsStack.Pop();
    }
    private void EndGame() 
    {
        Debug.Log("Game Over");
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
