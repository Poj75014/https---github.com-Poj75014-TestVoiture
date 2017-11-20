using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarControllerUpdateTesttNv2 : MonoBehaviour {

    public float idealRPM = 500f;
    public float maxRPM = 1000f;

    public Transform centerOfGravity;

    public WheelCollider wheelFR;
    public WheelCollider wheelFL;
    public WheelCollider wheelRR;
    public WheelCollider wheelRL;

    public float turnRadius = 6f;
    public float torque = 25f;
    public float brakeTorque = 100f;

    //public float AntiRoll = 20000.0f;

    public enum DriveMode { Front, Rear, All };
    public DriveMode driveMode = DriveMode.Rear;

    public Text speedText;
    private float SpeedKmh;

    public float m_Downforce = 100f;

    public bool StopVehicule = false;

    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0f, -0.9f, 0.2f); //Valeur initiale assez stable
    }

    public float Speed()
    {
        return wheelRR.radius * Mathf.PI * wheelRR.rpm * 60f / 1000f;
    }

    public float Rpm()
    {
        return wheelRL.rpm;
    }

    void FixedUpdate()
    {
        wheelFL.attachedRigidbody.AddForce(-transform.up * m_Downforce * wheelFL.attachedRigidbody.velocity.magnitude);

        if (speedText != null)
            speedText.text = "Speed: " + Speed().ToString("f0") + " km/h";
        //Debug.Log ("Speed: " + (wheelRR.radius * Mathf.PI * wheelRR.rpm * 60f / 1000f) + "km/h    RPM: " + wheelRL.rpm);

        //SpeedKmh = GetComponent<Rigidbody>().velocity.magnitude * 3.6f; //vitesse en km/h car on est en m/s sinon
        //speedText.text = "Speed : " + (int)SpeedKmh;

        float scaledTorque = Input.GetAxis("Vertical") * torque;

        if (wheelRL.rpm < idealRPM)
            scaledTorque = Mathf.Lerp(scaledTorque / 10f, scaledTorque, wheelRL.rpm / idealRPM);
        else
            scaledTorque = Mathf.Lerp(scaledTorque, 0, (wheelRL.rpm - idealRPM) / (maxRPM - idealRPM));

        //DoRollBar(wheelFR, wheelFL);
        //DoRollBar(wheelRR, wheelRL);

        wheelFR.steerAngle = Input.GetAxis("Horizontal") * turnRadius;
        wheelFL.steerAngle = Input.GetAxis("Horizontal") * turnRadius;

        wheelFR.motorTorque = driveMode == DriveMode.Rear ? 0 : scaledTorque;
        wheelFL.motorTorque = driveMode == DriveMode.Rear ? 0 : scaledTorque;
        wheelRR.motorTorque = driveMode == DriveMode.Front ? 0 : scaledTorque;
        wheelRL.motorTorque = driveMode == DriveMode.Front ? 0 : scaledTorque;

        if (Input.GetButton("Fire1"))
        {
            wheelFR.brakeTorque = brakeTorque;
            wheelFL.brakeTorque = brakeTorque;
            wheelRR.brakeTorque = brakeTorque;
            wheelRL.brakeTorque = brakeTorque;
        }
        else
        {
            wheelFR.brakeTorque = 0;
            wheelFL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
            wheelRL.brakeTorque = 0;
        }

        //Freiner le vehicule
        if (Input.GetKey(KeyCode.Space))
        {
            StopVehicule = true;
            wheelRL.brakeTorque = Mathf.Infinity;
            wheelRR.brakeTorque = Mathf.Infinity;
            wheelRL.brakeTorque = Mathf.Infinity;
            wheelRR.brakeTorque = Mathf.Infinity;
            wheelRL.motorTorque = 0;
            wheelRR.motorTorque = 0;
        }
        else
        {
            StopVehicule = false;
        }
    }


    /*void DoRollBar(WheelCollider WheelL, WheelCollider WheelR)
    {
        WheelHit hit;
        float travelL = 1.0f;
        float travelR = 1.0f;

        bool groundedL = WheelL.GetGroundHit(out hit);
        if (groundedL)
            travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) / WheelL.suspensionDistance;

        bool groundedR = WheelR.GetGroundHit(out hit);
        if (groundedR)
            travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius) / WheelR.suspensionDistance;

        float antiRollForce = (travelL - travelR) * AntiRoll;

        if (groundedL)
            GetComponent<Rigidbody>().AddForceAtPosition(WheelL.transform.up * -antiRollForce,
                                         WheelL.transform.position);
        if (groundedR)
            GetComponent<Rigidbody>().AddForceAtPosition(WheelR.transform.up * antiRollForce,
                                         WheelR.transform.position);
    }*/
}
