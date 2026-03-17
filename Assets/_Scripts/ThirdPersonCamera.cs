using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 thirdPersonOffset = new Vector3(0f, 2f, -4f);
    [SerializeField] private float smoothTime = 0.2f;

    private Vector3 currentVelocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPos = target.TransformPoint(thirdPersonOffset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}