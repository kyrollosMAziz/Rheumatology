using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class QuestionnaireController : MonoBehaviour, IQuestionAnswered
{
    public GameObject canvas;
    public TMP_Text questionText;
    public QuestionData currentQuestion;
    public Button[] answerButtons;


    private void Awake()
    {
        questionText.text = string.Empty;
        questionText.text = currentQuestion.questionText;
    }
    public void OnQuestionAnswered(int index)
    {
        CheckAnswer(index);}

    void CheckAnswer(int selectedIndex)
    {
        if (selectedIndex == currentQuestion.correctAnswerIndex)
        {
            answerButtons[selectedIndex].GetComponent<Image>().color = Color.green;
            #region ObserverImplementation
            //TODO: Observer Implementation
            //SceneManager.Instance.InvokeQuestionListeners();
            //SceneManager.Instance.RemoveQuestionAnsweredListeners();
            #endregion
            CorrectAnswer();
        }
        else
        {
            answerButtons[selectedIndex].GetComponent<Image>().color = Color.red;
            answerButtons[currentQuestion.correctAnswerIndex].GetComponent<Image>().color = Color.green;
        }
    }
    public void CorrectAnswer() 
    {
        DialoguesManager.Instance.StartDialogueSequenceHandler();
        QuestionsManager.Instance.HideQuestion();
    }
    public void OnQuestionAnswered()
    {
        //throw new System.NotImplementedException();
    }
}
