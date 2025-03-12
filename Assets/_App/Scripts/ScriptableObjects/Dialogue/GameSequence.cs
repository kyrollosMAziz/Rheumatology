using UnityEngine;

[CreateAssetMenu(fileName = "GameSequence", menuName = "Scriptable Objects/Dialogue_Sequence")]
public class GameSequence : ScriptableObject
{
    public AudioClip firstAudioClip;
    public AudioClip secondAudioClip;
}
