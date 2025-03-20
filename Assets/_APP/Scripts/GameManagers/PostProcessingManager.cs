using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : GenericSingleton<PostProcessingManager>
{
    [SerializeField] Volume volume;
    private Vignette vignette;
    private float intensity;

    void Start()
    {
        if (volume == null)
        {
            Debug.LogError("Volume is not assigned!");
            return;
        }

        if (volume.profile.TryGet(out vignette))
        {
            Debug.Log("Vignette effect found!");
            intensity = vignette.intensity.value;
        }
        ToggleVolume(false);
    }
    public void ToggleVolume(bool isEnable = true,UnityAction unityAction = null)
    {
        StartCoroutine(toggleVolume());

        IEnumerator toggleVolume()
        {
            if (isEnable)
            {
                volume.enabled = true;
                while (vignette.intensity.value != 1)
                {
                    intensity += 0.01f;
                    yield return new WaitForSeconds(0.01f);
                    vignette.intensity.Override(intensity);
                }
            }
            else
            {
                while (vignette.intensity.value != 0)
                {
                    intensity -= 0.01f;
                    yield return new WaitForSeconds(0.01f);
                    vignette.intensity.Override(intensity);
                }
                volume.enabled = false;
            }
            if (unityAction != null) unityAction();
        }
    }
}