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
        [SerializeField][Tooltip("Km/h")]
        private float m_maxSpeed;
        [SerializeField]
        private float m_motorBrake;
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

            DisplaySpeed();
        }

        void ApplyTorque()
        {
            float motorBrake = 0;
            float motorTorque = Input.GetAxis("Vertical") * m_maxMotorTorque;
            if (motorTorque == 0)
                motorBrake = m_motorBrake;
            if (m_axleInfos[0].leftWheel.attachedRigidbody.velocity.magnitude * 3.6f > m_maxSpeed)
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

        void DisplaySpeed()
        {
            float SpeedKmh = GetComponent<Rigidbody>().velocity.magnitude * 3.6f; //vitesse en km/h car on est en m/s sinon
            m_speedText.text = "Speed : " + (int)SpeedKmh;
        }
    }
}

