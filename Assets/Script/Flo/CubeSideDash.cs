using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSideDash : SideDash
{
    //public Vector3 velocity; //debug
    private Rigidbody vehicleRigidbody;


    // Use this for initialization
    private void Start()
    {
        base.Initialization();
        this.vehicleRigidbody = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //print(this.groundProperties.IsGrounded()); //debug

        // to delete ----------------------------------------------------
        //if (Input.GetKey(KeyCode.UpArrow)/* && this.groundProperties.IsGrounded()*/)
        //    this.transform.Translate(Vector3.forward);
        //if (Input.GetKey(KeyCode.DownArrow) /*&& this.groundProperties.IsGrounded()*/)
        //    this.transform.Translate(Vector3.back);

        this.vehicleRigidbody.velocity =  new Vector3(this.vehicleRigidbody.velocity.x, this.vehicleRigidbody.velocity.y, this.vehicleRigidbody.velocity.z + (Input.GetAxis("Vertical") * 0.5f));
        // --------------------------------------------------------------




        if (Input.GetKeyDown(KeyCode.A) /*&& this.groundProperties.IsGrounded() /*&& this.groundProperties != null*/)
            if (base.available)
                this.Jump(Direction.left);
        if (Input.GetKeyDown(KeyCode.E) /*&& this.groundProperties.IsGrounded() /*&& this.groundProperties != null*/)
            if (base.available)
                this.Jump(Direction.right);

        //velocity = this.GetComponent<Rigidbody>().velocity; //debug
    }    

    protected override void Jump(Direction direction)
    {
        Vector2 angle = MathsTools.DegreesToVector2(base.dashAngleInDegrees);
        //this.GetComponent<Rigidbody>().velocity = new Vector3(angle.x * (int)direction, -angle.y) * this.speed;
        Vector3 oldSpeed = this.vehicleRigidbody.velocity;
        this.vehicleRigidbody.velocity = (new Vector3(angle.x * (int)direction, -angle.y) * base.speed)
                                        + oldSpeed;
        this.vehicleRigidbody.useGravity = false;
        StartCoroutine(AutoStop(direction, oldSpeed));
        StartCoroutine(Cooldown());
    }

    private IEnumerator AutoStop(Direction direction, Vector3 oldSpeed)
    {
        yield return new WaitForSeconds(base.duration);
        this.vehicleRigidbody.velocity = oldSpeed;
        this.vehicleRigidbody.useGravity = true;
    }

    private IEnumerator Cooldown()
    {
        base.available = false;
        yield return new WaitForSeconds(base.cooldown);
        base.available = true;
        print("available");
    }

    private void OnValidate()
    {
        base.ValidateAttributes();
    }
}
