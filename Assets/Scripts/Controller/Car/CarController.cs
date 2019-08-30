using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;
    private float m_brakeInput;

    public WheelCollider wheel_frontLeft, wheel_frontRight, wheel_rearLeft, wheel_rearRight;
    public Transform T_wheel_frontLeft, T_wheel_frontRight, T_wheel_rearLeft, T_wheel_rearRight;
    public float maxSteeringAngle = 30f;
    public float motorForce = 50f;
    public float brakeTorque = 150f;

    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
        m_brakeInput = Input.GetAxis("Jump");
    }

    public void Steer()
    {
        m_steeringAngle = maxSteeringAngle * m_horizontalInput;
        wheel_frontLeft.steerAngle = m_steeringAngle;
        wheel_frontRight.steerAngle = m_steeringAngle;
    }

    private void Accelerate()
    {
        wheel_frontLeft.motorTorque = m_verticalInput * motorForce;
        wheel_frontRight.motorTorque = m_verticalInput * motorForce;
    }

    private void Break()
    {
        wheel_frontLeft.brakeTorque = brakeTorque * m_brakeInput;
        wheel_frontRight.brakeTorque = brakeTorque * m_brakeInput;
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;
        _collider.GetWorldPose(out _pos, out _quat);
        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(wheel_frontLeft, T_wheel_frontLeft);
        UpdateWheelPose(wheel_frontRight, T_wheel_frontRight);
        UpdateWheelPose(wheel_rearLeft, T_wheel_rearLeft);
        UpdateWheelPose(wheel_rearRight, T_wheel_rearRight);
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        Break();
        UpdateWheelPoses();
    }
}
