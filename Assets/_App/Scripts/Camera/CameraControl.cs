using UnityEngine;

public class CameraControl : GenericSingleton<CameraControl>
{
    [Header("Mouse Sensitivity")]
    public float sensitivityX = 2f;
    public float sensitivityY = 2f;

    [Header("Rotation Limits")]
    public float minXRotation = -60f; 
    public float maxXRotation = 60f;  
    public float minYRotation = -90f; 
    public float maxYRotation = 90f;

    public bool isCameraControl;

    private float rotationX = 0f;
    private float rotationY = 0f;
    private Transform originalTransform;

    private void Awake()
    {
        originalTransform = transform;
        isCameraControl = true;
    }

    void Update()
    {
        if (isCameraControl)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

            rotationX -= mouseY;
            rotationY += mouseX;

            rotationX = Mathf.Clamp(rotationX, minXRotation, maxXRotation);
            rotationY = Mathf.Clamp(rotationY, minYRotation, maxYRotation);

            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        }
    }
    public void LockCamera() 
    {
        isCameraControl = false;
        transform.position = originalTransform.position; 
        transform.rotation = originalTransform.rotation;
    }
    public void UnlockCamera() 
    {
        isCameraControl = true;
    }
}
