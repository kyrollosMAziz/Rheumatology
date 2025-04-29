using UnityEngine;

[CreateAssetMenu(fileName = "GameSequence", menuName = "Scriptable Objects/Dialogue_Sequence")]
public class GameSequence : ScriptableObject
{
    public AudioClip firstAudioClip;
    public string firstAudioClipText;
    public AudioClip secondAudioClip;
    public string secondAudioClipText;
    public string patientAnimationState;
    public string doctorAnimationState;
}
