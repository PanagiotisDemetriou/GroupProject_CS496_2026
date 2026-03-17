using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] private Transform target;
    [SerializeField, Range(1f, 20f)] private float orbitDistance = 5f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float heightOffset = 1.8f;

    private float yaw;
    private float pitch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (target != null)
        {
            yaw = target.eulerAngles.y;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // Mouse input
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, 0, 80f); // prevent flipping
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 lookPoint = target.position + Vector3.up * heightOffset;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 position = lookPoint - (rotation * Vector3.forward * orbitDistance);

        transform.position = position;
        transform.LookAt(lookPoint);
    }
}
