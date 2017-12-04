using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCarController : MonoBehaviour {

    public List<Wheels> WheelsInfos;

    private Rigidbody Rb;
    public float Downforce = 100f;

    public float MotorTorque;
    public float SteerAngle;

    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0f, -0.9f, 0.2f); //Valeur initiale assez stable
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
            if (WheelsInfos.DForce)
            {
                Rb.AddForce(-transform.up * Downforce * Rb.velocity.magnitude);
            }
        }
	}

    [System.Serializable]
    public class Wheels
    {
        public WheelCollider LeftWheels;
        public WheelCollider RightWheels;
        public bool motor;
        public bool stearing;
        public bool DForce;
    }
}

