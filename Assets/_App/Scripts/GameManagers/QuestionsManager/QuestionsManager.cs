using System.Collections.Generic;
using UnityEngine;

public class QuestionsManager : GenericSingleton<QuestionsManager>
{
    public List<GameObject> questions;
    LinkedList<GameObject> questionsLinkedList;
    LinkedListNode<GameObject> currentNode;

    private void Awake()
    {
        foreach (Transform child in transform) 
        {
            questions.Add(child.gameObject);
        }
        questionsLinkedList = new LinkedList<GameObject>(questions);
    }
    public void OnQuestionAnswered(string index)
    {
        GameSequencePhase gameSequencePhase = (GameSequencePhase)int.Parse(index);
        SceneManager.Instance.LoadNextSequence(gameSequencePhase);
    }
    public void EnableNextQuestion() 
    {
        if (currentNode == null) 
        {
            Debug.Log("first question");
            currentNode = questionsLinkedList.First;
            return;
        }
        else if (currentNode.Next == null) 
        {
            Debug.Log("Last Question");
        }
        currentNode.Value.gameObject.SetActive(false);
        currentNode = currentNode.Next;
        //currentNode.Value.gameObject.SetActive(true);
    }
    public void ShowQuestion() 
    {
        if( currentNode != null && currentNode.Value!= null) currentNode.Value.gameObject.SetActive(true);
    }
    public void HideQuestion()
    {
        if (currentNode != null  && currentNode.Value != null) currentNode.Value.gameObject.SetActive(false);
    }
    public string GetQuestionIndex() 
    {
        var questionnaire = currentNode.Value.GetComponent<QuestionnaireController>();
        GameSequencePhase questionPhase = questionnaire.questionPhase;
        int index = (int)questionPhase;

        return index.ToString();
    }
}
