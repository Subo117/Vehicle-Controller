using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform carPos;
    [SerializeField] Transform CameraPoint;

    Vector3 velocity = Vector3.zero;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.LookAt(carPos);
        transform.position = Vector3.SmoothDamp(transform.position, CameraPoint.position, ref velocity, Time.deltaTime * 5f);
    }
}
