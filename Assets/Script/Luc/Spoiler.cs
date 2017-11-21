using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spoiler : MonoBehaviour {

    [SerializeField]
    private AnimationCurve m_force;
    [SerializeField]
    private Rigidbody m_rigidbody;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        m_rigidbody.AddForce(m_force.Evaluate(transform.InverseTransformVector(m_rigidbody.velocity).z * 3.6f) * new Vector3(0,1,0));

    }
}
