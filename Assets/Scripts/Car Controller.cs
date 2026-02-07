using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] float motorPower = 100f;
    [SerializeField] float steeringAngle = 30f;
    [SerializeField] Transform carCOMTransform;

    public WheelColliders wheelCollider;
    public WheelMeshes wheelMeshes;

    public Vector3 playerMovementInput;

    Rigidbody playerRB;

    public float speed;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRB.centerOfMass = carCOMTransform.localPosition;
    }

    private void FixedUpdate()
    {
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
        wheelCollider.frontLeft.motorTorque = motorPower * playerMovementInput.z;
        wheelCollider.frontRight.motorTorque = motorPower * playerMovementInput.z;
    }

    void ApplySteering()
    {
        wheelCollider.frontLeft.steerAngle = steeringAngle * playerMovementInput.x;
        wheelCollider.frontRight.steerAngle = steeringAngle * playerMovementInput.x;
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
