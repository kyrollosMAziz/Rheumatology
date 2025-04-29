using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class DialoguesManager : GenericSingleton<DialoguesManager>
{
    public GameSequence currentSequence;
    public float delay;
    AudioSource audioSource;
#if !UNITY_EDITOR && UNITY_WEBGL
      [DllImport("__Internal")]
      private static extern void OpenQuestion(string questionIndex);
#endif
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

        if (m_delay > 0) //overwrite based on sequence
        {
            delay = m_delay;
        }

        StartCoroutine(
            PlayAudio(currentSequence.firstAudioClip, currentSequence.firstAudioClipText, () => OnAudioEnd()));
    }

    IEnumerator PlayAudio(AudioClip audioClip, string subtitleText, UnityAction onAudioEnd)
    {
        yield return new WaitForSeconds(delay);
        //Place black FadeScreen here

        audioSource.clip = audioClip;
        audioSource.Play();
        SubtitleManager.Instance.PopulateText(subtitleText);
        audioSource.loop = false;
        yield return new WaitForSeconds(audioClip.length);
        SubtitleManager.Instance.HideCanvas();
        onAudioEnd();
    }

    private void OnAudioEnd()
    {
        if (currentSequence.secondAudioClip == null)
        {
            LoadNextQuestion();
            return;
        }
        StartCoroutine(PlayAudio(currentSequence.secondAudioClip, currentSequence.secondAudioClipText,
            () => { LoadNextQuestion(); }));
    }

    //TODO: Marwan => Open Question from Boda's front end
    private void LoadNextQuestion()
    {
        SceneManager.Instance.IsCursorActive = true;
        QuestionsManager.Instance.EnableNextQuestion();
        var questionIndex = QuestionsManager.Instance.GetQuestionIndex();
        if (!string.IsNullOrEmpty(questionIndex))
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            OpenQuestion(questionIndex);
#endif
        }

        currentSequence = SceneManager.Instance.PopGameSequence();
    }
}