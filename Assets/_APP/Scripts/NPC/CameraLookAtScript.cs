//using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraLookAtScript : MonoBehaviour
{
    [SerializeField]
    private Transform CameraLookAtObject;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(CameraLookAtObject, Vector3.right);
    }
}
