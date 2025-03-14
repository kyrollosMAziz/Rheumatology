using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : GenericSingleton<UIManager>
{
    public Canvas endGameCanvas;

    public Button Restart1, Restart2;

    private void Awake()
    {
        Restart1.onClick.AddListener(() =>
        {
            RestartGame();
        });
        Restart2.onClick.AddListener(() =>
        {
            RestartGame();
        });
    }
    private void RestartGame() 
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Restart1.onClick = null;
        Restart2.onClick = null;
    }
    public void ShowEndGameCanvas()
    {
        endGameCanvas.enabled = true;
    }
    public void HideEndGameCanvas()
    {
        endGameCanvas.enabled = false;
    }


}
