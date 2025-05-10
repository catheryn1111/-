using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    [SerializeField] List<AxleInfo> axleInfos; // èíôîðìàöèÿ î êàæäîé îñè
    [SerializeField] float maxMotorTorque; // ìàêñèìàëüíûé êðóòÿùèé ìîìåíò
    [SerializeField] float maxSteeringAngle; // ìàêñèìàëüíûé óãîë ïîâîðîòà êîëåñ
    [SerializeField] Joystick joystick;
    bool isBreak;
    // bool
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }
        Transform visualWheel = collider.transform.GetChild(0);
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * joystick.Vertical;
        float steering = maxSteeringAngle * joystick.Horizontal;
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            if (!isBreak)
            {
                axleInfo.leftWheel.brakeTorque = 0;
                axleInfo.rightWheel.brakeTorque = 0;
            }
            else
            {
                axleInfo.leftWheel.brakeTorque = 100;
                axleInfo.rightWheel.brakeTorque = 100;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);

        }
    }
    public void StopOn()
    {
        isBreak = true;
    }
    public void StopOff()
    {
        isBreak = false;
    }
}
[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // ïðèñîåäèíåíî ëè êîëåñî ê ìîòîðó?
    public bool steering; // ïîâîðà÷èâàåò ëè ýòî êîëåñî?
}
