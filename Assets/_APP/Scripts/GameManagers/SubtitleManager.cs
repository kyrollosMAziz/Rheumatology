using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleManager : SceneContextSingleton<SubtitleManager>
{
   [SerializeField] private TextMeshProUGUI _subtitleText;

   public void PopulateText(string subtitleText)
   {
      _subtitleText.text = subtitleText;
      ShowCanvas();
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
