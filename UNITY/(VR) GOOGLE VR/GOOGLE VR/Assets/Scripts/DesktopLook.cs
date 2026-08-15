using UnityEngine;

public class DesktopLook : MonoBehaviour
{
    public Transform cameraTransform;
    public float sensitivity = 3f;

    private float pitch = 0f;

    void Start()
    {
#if UNITY_STANDALONE_WIN
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#endif
    }

    void Update()
    {
#if UNITY_STANDALONE_WIN
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Giro horizontal: Player completo
        transform.Rotate(0f, mouseX, 0f);

        // Giro vertical: solamente la cámara
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -85f, 85f);

        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
#endif
    }
}