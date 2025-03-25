using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebglFocusManager : ProjectContextSingleton<WebglFocusManager>
{
    public void DisableUnityFocus()
    {
        print("UnityFocus: Disabled");
#if !UNITY_EDITOR && UNITY_WEBGL
    WebGLInput.captureAllKeyboardInput = false;
#endif
    }

    public void EnableUnityFocus()
    {
        print("UnityFocus: Enabled");
#if !UNITY_EDITOR && UNITY_WEBGL
    WebGLInput.captureAllKeyboardInput = true;
#endif
    }
}