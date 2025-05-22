using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenFader : MonoBehaviour
{
    [SerializeField] CanvasGroup Loading_CanvasGroup;
    [SerializeField] Image Loading_ScreenBackground;

    private void Start()
    {
        StartCoroutine(canvasGroupFader());
        IEnumerator canvasGroupFader()
        {
            bool isFading = true;
            while (isFading)
            {
                yield return new WaitForEndOfFrame();
                Loading_CanvasGroup.alpha -= 0.01f;
                if (Loading_CanvasGroup.alpha == 0) 
                {
                    OnGameStart();
                    yield break;
                }
            }
        }
    }
    private void OnGameStart() 
    {
        SceneManager.Instance.StartGame();
        Loading_ScreenBackground.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
