using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Architecture
{
    class CarController : MonoBehaviour
    {
        private List<WheelSetting> m_wheelSetting;
        private float m_motorBrake;
        private float m_brake;
        private float m_maxSteer;
        private AnimationCurve m_steerFactorBySpeed;
        private float m_maxMotorTorque;
        private AnimationCurve m_motorTroqueFactorBySpeed;

        public WheelSetting WheelSetting
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }
    }
}
