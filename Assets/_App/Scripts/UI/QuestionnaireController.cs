using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class QuestionnaireController : MonoBehaviour
{
    public GameObject canvas;
    public TMP_Text questionText;
    public QuestionData currentQuestion;
    public Button[] answerButtons;
    public GameSequencePhase questionPhase;

    private void Awake()
    {
        questionText.text = string.Empty;
        questionText.text = currentQuestion.questionText;
    }
    public void OnQuestionAnswered(string index)
    {
        GameSequencePhase gameSequencePhase = (GameSequencePhase) int.Parse(index);
        SceneManager.Instance.LoadNextSequence(gameSequencePhase);
    }
    public void OnQuestionAnswered(int index)
    {
        CorrectAnswer();
    }
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
        //TODO: Boda => Boda sends us the Answer clicked in unity
        SceneManager.Instance.LoadNextSequence(questionPhase);
    }
    public void OnQuestionAnswered()
    {
        //throw new System.NotImplementedException();
    }
}
