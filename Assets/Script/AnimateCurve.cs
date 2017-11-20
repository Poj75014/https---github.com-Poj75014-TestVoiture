using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCurve : MonoBehaviour {

    public AnimationCurve Forcecurve;

    void Start()
    {
        Forcecurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, Forcecurve.Evaluate(Time.time), transform.position.z);
    }
}
