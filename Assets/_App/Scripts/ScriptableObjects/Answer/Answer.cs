using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    public UnityEvent onAnswerPressed;
    public int _AnswerIndex;
    public QuestionnaireController _root;

    private void Awake()
    {
        onAnswerPressed = GetComponent<Button>().onClick;
        onAnswerPressed.AddListener(() => _root.OnQuestionAnswered(_AnswerIndex));    
    }
}
