using UnityEngine;

public enum AnimationTarget
{
    Doctor,
    Patient,
}

[CreateAssetMenu(fileName = "GameSequence", menuName = "Scriptable Objects/Dialogue_Sequence")]
public class GameSequence : ScriptableObject
{
    public AudioClip firstAudioClip;
    public string firstAudioClipText;
    public AnimationTarget firstAudioClipAnimationTarget;
    public string firstAudioClipAnimationState;
    
    public AudioClip secondAudioClip;
    public string secondAudioClipText;
    public AnimationTarget secondAudioClipAnimationTarget;
    public string secondAudioClipAnimationState;
   
}