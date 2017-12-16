using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CarUserControl : MonoBehaviour
{
    private Assets.Architecture.CarController m_carController;

    internal Assets.Architecture.CarController CarController
    {
        get
        {
            return m_carController;
        }

        set
        {

        }
    }

    public Skill Skill
    {
        get
        {
            throw new System.NotImplementedException();
        }

        set
        {
        }
    }

    private void Update()
    {

    }
}