﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarControllerNV2 : MonoBehaviour {

    public Text SpeedText;
    public WheelCollider Front_Left;
    public WheelCollider Front_Right;
    public WheelCollider Back_Left;
    public WheelCollider Back_Right;

    public float Torque;
    public float Speed;
    public float MaxSpeed = 200f; //la vitesse maximum
    public int Brake = 10000; //le freinage
    public float Acceleration = 10f; //le pourcentage d'acceleration du vehicule
    public float WheelAngleMax = 10f; //permettra de tourner le vehicule
    public float ADMax = 40f; //permettre d'avoir une meilleur tenue de route pour les roues. ADMax --> AssistDirectionMax
    public bool StopVehicule = false; //pour freiner le véhicule
    private float m_Downforce = 100f;

    void Start()
    {
        //le centre de masse
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0f, -0.9f, 0.2f); //Valeur initiale assez stable

        //Accrochege au sol
            Front_Left.attachedRigidbody.AddForce(-transform.up * m_Downforce * Front_Left.attachedRigidbody.velocity.magnitude);
    }


    void Update () {

        //Bruitage moteur
        float Valeur_pitch = Speed / MaxSpeed + 1.5f;
        GetComponent<AudioSource>().pitch = Mathf.Clamp(Valeur_pitch, 1f, 2.5f);

        //Affichage de la vitesse
        Speed = GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
        SpeedText.text = "Speed : " + (int)Speed;

        //Acceleration du vehicule
        if (Input.GetKey(KeyCode.UpArrow) && Speed < MaxSpeed)
        {
            Back_Left.brakeTorque = 0;
            Back_Right.brakeTorque = 0;
            Front_Left.brakeTorque = 0;
            Front_Right.brakeTorque = 0;
            Front_Left.motorTorque = Input.GetAxis("Vertical") * Torque * Acceleration * Time.deltaTime;
            Front_Right.motorTorque = Input.GetAxis("Vertical") * Torque * Acceleration * Time.deltaTime;
        }

        //Décélération du véhicule
        if (!Input.GetKey(KeyCode.UpArrow) && !StopVehicule || Speed > MaxSpeed)
        {
            Front_Left.motorTorque = 0;
            Front_Right.motorTorque = 0;
            Front_Left.brakeTorque = Brake * Acceleration * Time.deltaTime;
            Front_Right.brakeTorque = Brake * Acceleration * Time.deltaTime;
        }

        //Direction du vehicule
        float AD = (((WheelAngleMax - ADMax) / MaxSpeed) * Speed) + ADMax;
        Debug.Log(AD);

        Front_Left.steerAngle = Input.GetAxis("Horizontal") * AD;
        Front_Right.steerAngle = Input.GetAxis("Horizontal") * AD;

        //Freiner le vehicule
        if (Input.GetKey(KeyCode.Space))
        {
            StopVehicule = true;
            //Backlight.SetActive(true);
            Back_Left.brakeTorque = Mathf.Infinity;
            Back_Right.brakeTorque = Mathf.Infinity;
            Front_Left.brakeTorque = Mathf.Infinity;
            Front_Right.brakeTorque = Mathf.Infinity;
            Front_Left.motorTorque = 0;
            Front_Right.motorTorque = 0;
        }
        else
        {
            StopVehicule = false;
            //Backlight.SetActive(false);
        }

        //Marche arriere
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Back_Left.brakeTorque = 0;
            Back_Right.brakeTorque = 0;
            Front_Left.brakeTorque = 0;
            Front_Right.brakeTorque = 0;
            Front_Left.motorTorque = Input.GetAxis("Vertical") * Torque * Acceleration * Time.deltaTime;
            Front_Right.motorTorque = Input.GetAxis("Vertical") * Torque * Acceleration * Time.deltaTime;
        }
    }

}
