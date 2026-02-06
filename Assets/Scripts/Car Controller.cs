using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] float motorPower = 100f;
    public AnimationCurve steeringCurve;

    public WheelColliders wheelCollider;
    public WheelMeshes wheelMeshes;

    public Vector3 playerMovementInput;

    Rigidbody playerRB;

    public float speed;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        speed = playerRB.linearVelocity.magnitude;
        UpdateWheel();
        ApplyMovement();
        ApplySteering();
    }

    void UpdateWheel()
    {
        UpdateWheelPos(wheelCollider.frontLeft, wheelMeshes.frontLeft);
        UpdateWheelPos(wheelCollider.frontRight, wheelMeshes.frontRight);
        UpdateWheelPos(wheelCollider.rearLeft, wheelMeshes.rearLeft);
        UpdateWheelPos(wheelCollider.rearRight, wheelMeshes.rearRight);
    }
    void UpdateWheelPos(WheelCollider wheel, MeshRenderer mesh)
    {
        Quaternion quat;
        Vector3 pos;
        wheel.GetWorldPose(out pos, out quat);

        mesh.transform.position = pos;
        mesh.transform.rotation = quat;
    }

    public void GetPlayerInput(Vector2 input)
    {
        playerMovementInput.z = input.y;
        playerMovementInput.x = input.x;
    }

    void ApplyMovement()
    {
        wheelCollider.rearLeft.motorTorque = playerMovementInput.z * motorPower;
        wheelCollider.rearRight.motorTorque = playerMovementInput.z * motorPower;
    }

    void ApplySteering()
    {
        float steeringAngle = steeringCurve.Evaluate(speed) * playerMovementInput.x;
        wheelCollider.frontLeft.steerAngle = steeringAngle;
        wheelCollider.frontRight.steerAngle = steeringAngle;
    }

}

[System.Serializable]
public class WheelColliders
{
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;
}
[System.Serializable]
public class WheelMeshes
{
    public MeshRenderer frontLeft;
    public MeshRenderer frontRight;
    public MeshRenderer rearLeft;
    public MeshRenderer rearRight;
}
