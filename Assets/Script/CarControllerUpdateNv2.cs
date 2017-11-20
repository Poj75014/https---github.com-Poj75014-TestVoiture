using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarControllerUpdateNv2 : MonoBehaviour {

    public Text SpeedText;
    public float Speed;

    public List<Mapping> Mappings;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    void Start()
    {
        //le centre de masse
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0f, -0.9f, 0.2f); //Valeur initiale assez stable
    }


    void FixedUpdate () {

        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steer = maxSteeringAngle * Input.GetAxis("Horizontal");

        //Affichage de la vitesse
        Speed = GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
        SpeedText.text = "Speed : " + (int)Speed;

        foreach (Mapping axis in Mappings)
        {
            if (axis.Verif_steer)
            {
                axis.leftWheel.steerAngle = steer;
                axis.rightWheel.steerAngle = steer;
            }
            if (axis.Verif_motor)
            {
                axis.leftWheel.motorTorque = motor;
                axis.rightWheel.motorTorque = motor;
            }
        }
    }



    [System.Serializable]
    public class Mapping
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool Verif_motor;
        public bool Verif_steer;
    }
}
