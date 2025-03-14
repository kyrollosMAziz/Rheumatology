using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DialoguesManager : GenericSingleton<DialoguesManager>
{
    public GameSequence currentSequence;
    public float delay;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void StartDialogueSequenceHandler(float m_delay = 0)
    {
        SceneManager.Instance.IsCursorActive = false;
        if (!currentSequence) 
        {
            SceneManager.Instance.EndGame();
            return;
        }
        if (m_delay > 0)//overwrite based on sequence
        {
            delay = m_delay;
        }
        StartCoroutine(PlayAudio(currentSequence.firstAudioClip, () => OnAudioEnd()));
    }
    IEnumerator PlayAudio(AudioClip audioClip, UnityAction onAudioEnd)
    {
        yield return new WaitForSeconds(delay);
        //Place black FadeScreen here
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.loop = false;
        yield return new WaitForSeconds(audioClip.length);
        onAudioEnd();
    }
    private void OnAudioEnd()
    {
        if (currentSequence.secondAudioClip == null)
        {
            LoadNextQuestion();
            return;
        }
        StartCoroutine(PlayAudio(currentSequence.secondAudioClip, () =>
        {
            LoadNextQuestion();
        }));
    }
    private void LoadNextQuestion() 
    {
        SceneManager.Instance.IsCursorActive = true;
        QuestionsManager.Instance.EnableNextQuestion();
        currentSequence = SceneManager.Instance.PopGameSequence();
    }
}
