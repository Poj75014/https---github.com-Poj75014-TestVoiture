using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDash : SideDash
{

    // Use this for initialization
    private void Start()
    {
        base.Initialization();
    }

    // Update is called once per frame
    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.A) /*&& this.groundProperties.IsGrounded() /*&& this.groundProperties != null*/)
            this.Jump(Direction.left);
        if (Input.GetKeyDown(KeyCode.E) /*&& this.groundProperties.IsGrounded() /*&& this.groundProperties != null*/)
            this.Jump(Direction.right);
    }

    protected override void Jump(Direction direction)
    {
        Vector2 angle = MathsTools.DegreesToVector2(this.dashAngleInDegrees);
        this.GetComponent<Rigidbody>().velocity = transform.InverseTransformVector(new Vector3(angle.x * (int)direction, -angle.y/*, this.GetComponent<Rigidbody>().velocity.z*/)) * this.speed;
    }

    private void OnValidate()
    {
        base.ValidateAttributes();
    }
}
