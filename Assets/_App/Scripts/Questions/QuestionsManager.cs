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

    public void EnableNextQuestion() 
    {
        if (currentNode == null) 
        {
            Debug.Log("first question");
            currentNode = questionsLinkedList.First;
            currentNode.Value.gameObject.SetActive(true);
            return;
        }
        else if (currentNode.Next == null) 
        {
            Debug.Log("Game Over");
            return;
        }
        currentNode.Value.gameObject.SetActive(false);
        currentNode = currentNode.Next;
        currentNode.Value.gameObject.SetActive(true);
    }
}
