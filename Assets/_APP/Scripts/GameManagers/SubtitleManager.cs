using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleManager : SceneContextSingleton<SubtitleManager>
{
   [SerializeField] private TextMeshProUGUI _subtitleText;
   [SerializeField] private float typingSpeed = 0.05f;

   public void PopulateText(string subtitleText)
   {
      _subtitleText.text = string.Empty;
      ShowCanvas();
      StartCoroutine(TypeText(subtitleText));
   }
   
   IEnumerator TypeText(string textToShow)
   {
      for (int i = 0; i < textToShow.Length; i++)
      {
         _subtitleText.text += textToShow[i];
         yield return new WaitForSeconds(typingSpeed);
      }
   }

   public void ShowCanvas()
   {
      GetComponent<Canvas>().enabled = true;
   }
   
   
   public void HideCanvas()
   {
      GetComponent<Canvas>().enabled = false;
   }
}
