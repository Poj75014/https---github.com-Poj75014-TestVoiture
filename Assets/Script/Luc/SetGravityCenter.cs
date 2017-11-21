using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGravityCenter : MonoBehaviour {

    [SerializeField]
    private Vector3 m_gravityCenter;

    private void FixedUpdate()//pas bien
    {
        GetComponent<Rigidbody>().centerOfMass = m_gravityCenter;
    }
}
