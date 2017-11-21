using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rework
{
    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor;
        public bool steering;
    }

    public class BasicCarController : MonoBehaviour
    {
        [SerializeField]
        private float m_maxMotorTorque;
        [SerializeField] [Tooltip("Km/h")]
        private float m_maxSpeed;
        [SerializeField]

        private float m_brake;
        [SerializeField]
        private float m_motorBrake;

        [SerializeField]
        private float m_maxSteer;

        [SerializeField]
        private AnimationCurve m_steerFactorBySpeed = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0.2f));

        [SerializeField]
        private List<AxleInfo> m_axleInfos;

        [SerializeField]
        private Text m_speedText;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {


            ApplyTorque();
            ApplySteer();
            DisplaySpeed();
        }

        void ApplyTorque()
        {
            float motorBrake = 0;
            float motorTorque = Input.GetAxis("Vertical") * m_maxMotorTorque;
            if (motorTorque == 0)
                motorBrake = m_motorBrake;

            if(motorTorque < 0 && transform.InverseTransformVector(m_axleInfos[0].leftWheel.attachedRigidbody.velocity).z > 0)
            {
                motorBrake = m_brake;
                motorTorque = 0;
            }
            else if (motorTorque > 0 && transform.InverseTransformVector(m_axleInfos[0].leftWheel.attachedRigidbody.velocity).z < 0)
            {
                motorBrake = m_brake;
                motorTorque = 0;
            }


            if (transform.InverseTransformVector(m_axleInfos[0].leftWheel.attachedRigidbody.velocity).z * 3.6f > m_maxSpeed)
                motorTorque = 0;

            if (-transform.InverseTransformVector(m_axleInfos[0].leftWheel.attachedRigidbody.velocity).z * 3.6f > m_maxSpeed/5)
                motorTorque = 0;

            for (int i = 0; i < m_axleInfos.Count; i++)
            {
                if (m_axleInfos[i].motor)
                {
                    if(m_axleInfos[i].leftWheel != null)
                    {
                        m_axleInfos[i].leftWheel.motorTorque = motorTorque;
                        m_axleInfos[i].leftWheel.brakeTorque = motorBrake;
                    }
                    if(m_axleInfos[i].rightWheel != null)
                    {
                        m_axleInfos[i].rightWheel.motorTorque = motorTorque;
                        m_axleInfos[i].rightWheel.brakeTorque = motorBrake;
                    }
                }
            }
        }

        void ApplySteer()
        {
            float hInput = Input.GetAxis("Horizontal");
            float steerFactor = m_steerFactorBySpeed.Evaluate(transform.InverseTransformVector(m_axleInfos[0].leftWheel.attachedRigidbody.velocity).z * 3.6f / m_maxSpeed);

            float steer = hInput * m_maxSteer * steerFactor;


            for (int i = 0; i < m_axleInfos.Count; i++)
            {
                if (m_axleInfos[i].steering)
                {
                    if (m_axleInfos[i].leftWheel != null)
                    {
                        m_axleInfos[i].leftWheel.steerAngle = steer;
                    }
                    if (m_axleInfos[i].rightWheel != null)
                    {
                        m_axleInfos[i].rightWheel.steerAngle = steer;
                    }
                }
            }
        }

        void DisplaySpeed()
        {
            float SpeedKmh = GetComponent<Rigidbody>().velocity.magnitude * 3.6f; //vitesse en km/h car on est en m/s sinon
            m_speedText.text = "Speed : " + (int)SpeedKmh;
        }
    }
}

