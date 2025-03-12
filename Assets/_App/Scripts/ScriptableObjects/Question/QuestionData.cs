using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestion", menuName = "Scriptable Objects/Question")]
public class QuestionData : ScriptableObject
{
    [TextArea] public string questionText;
    //public string[] answers = new string[4];
    public int correctAnswerIndex;
}
