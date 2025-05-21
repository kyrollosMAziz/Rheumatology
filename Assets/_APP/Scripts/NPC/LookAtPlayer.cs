using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    [SerializeField]
    Transform playerCamera;

    [SerializeField]
    Vector3 orientation = new Vector3(0.0f, 1.0f, 0.0f);
    [SerializeField, Range(0,1)]    
    float influnce = 1.0f;

    [SerializeField]
    float angleFix = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {   
        Quaternion rotation = transform.rotation;
        transform.LookAt(playerCamera, orientation);
        
        transform.Rotate(orientation, angleFix);

        transform.rotation = Quaternion.Slerp(rotation, transform.rotation, influnce);
    }
}
