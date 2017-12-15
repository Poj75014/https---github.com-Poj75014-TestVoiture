﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCarController : MonoBehaviour {

    public List<Wheels> WheelsInfos;

    public float Downforce = 100f;

    public float MotorTorque;
    public float SteerAngle;

    void Start()
    {
        //center of mass
        //GetComponent<Rigidbody>().centerOfMass = new Vector3(0f, -1f, 0.2f);//Valeur aaser stable
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0f, 1.4f, 0.4f);
    }

    void FixedUpdate () {

        //Rb.AddForce(-transform.up * Downforce * Rb.velocity.magnitude);

        float motor = MotorTorque * Input.GetAxis("Vertical");
        float stearing = SteerAngle * Input.GetAxis("Horizontal");

        foreach (Wheels WheelsInfos in WheelsInfos)
        {
            if (WheelsInfos.stearing)
            {
                WheelsInfos.LeftWheels.steerAngle = stearing;
                WheelsInfos.RightWheels.steerAngle = stearing;
            }
            if (WheelsInfos.motor)
            {
                WheelsInfos.LeftWheels.motorTorque = motor;
                WheelsInfos.RightWheels.motorTorque = motor;
            }
            { 
                Vector3 wheelWorldPoseL;
                Quaternion wheelWorldRotL;
                Vector3 WheelWhorldPoseR;
                Quaternion wheelWorldRotR;

                WheelsInfos.LeftWheels.GetWorldPose(out wheelWorldPoseL, out wheelWorldRotL);
                WheelsInfos.RightWheels.GetWorldPose(out WheelWhorldPoseR, out wheelWorldRotR);

                WheelsInfos.LWheels.position = wheelWorldPoseL;
                WheelsInfos.LWheels.rotation = wheelWorldRotL;
                WheelsInfos.RWheels.position = WheelWhorldPoseR;
                WheelsInfos.RWheels.rotation = wheelWorldRotR;
            }

            if (WheelsInfos.DForce)
            {
                WheelsInfos.LeftWheels.attachedRigidbody.AddForce(-transform.up * Downforce * WheelsInfos.LeftWheels.attachedRigidbody.velocity.magnitude);
            }
        }
	}

    [System.Serializable]
    public class Wheels
    {
        public WheelCollider LeftWheels;
        public WheelCollider RightWheels;
        public Transform LWheels;
        public Transform RWheels;
        public bool motor;
        public bool stearing;
        public bool DForce;
    }
}
