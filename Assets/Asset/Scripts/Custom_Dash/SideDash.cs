using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(/*typeof(IGround),*/ typeof(Rigidbody))]
public abstract class SideDash : MonoBehaviour
{
    // ATTRIBUTES
    public float speed;
    public float dashAngleInDegrees;

    //protected IGround groundProperties;
    
    protected enum Direction { left = -1, right = 1 };
    
    // METHODS
    protected abstract void Jump(Direction direction);
    
    protected virtual void Initialization()
    {
        //this.groundProperties = this.gameObject.GetComponent(typeof(IGround)) as IGround;
    }

    protected virtual void ValidateAttributes()
    {
        this.dashAngleInDegrees = Mathf.Clamp(this.dashAngleInDegrees, 0f, 90f);
        this.speed = Mathf.Max(this.speed, 0f);
    }
}
