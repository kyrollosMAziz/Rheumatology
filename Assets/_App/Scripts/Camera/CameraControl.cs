using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Mouse Sensitivity")]
    public float sensitivityX = 2f;
    public float sensitivityY = 2f;

    [Header("Rotation Limits")]
    public float minXRotation = -60f; // Minimum vertical angle
    public float maxXRotation = 60f;  // Maximum vertical angle
    public float minYRotation = -90f; // Minimum horizontal angle
    public float maxYRotation = 90f;  // Maximum horizontal angle

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked; // Locks the cursor in the center
        //Cursor.visible = false; // Hides the cursor
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        // Apply rotation
        rotationX -= mouseY; // Invert for natural FPS feel
        rotationY += mouseX;

        // Clamp rotation to prevent over-rotation
        rotationX = Mathf.Clamp(rotationX, minXRotation, maxXRotation);
        rotationY = Mathf.Clamp(rotationY, minYRotation, maxYRotation);

        // Apply rotation to the transform
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}
