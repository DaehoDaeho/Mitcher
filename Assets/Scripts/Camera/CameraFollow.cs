using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smoothSpeed = 5.0f;

    [SerializeField]
    private Vector3 offset = new Vector3(0.0f, 0.0f, -10.0f);

    // Update is called once per frame
    void LateUpdate()
    {
        if(target == null)
        {
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, smoothedPosition.z);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
