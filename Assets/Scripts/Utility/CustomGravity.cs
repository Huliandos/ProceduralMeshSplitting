
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Script that enables a custom Gravity to objects without using the Gravity implemented into Rigidbodies
    3D Rigidbodies don't suppoer gravity scales to modify the effect of gravity on this object
    This is used in conjuction with better jumping
*/

public class CustomGravity : MonoBehaviour
{
    //modifier to the gravity
    float gravityScale = 1.0f;

    float gravity = -9.81f;

    Rigidbody rb;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravityVector = gravity * gravityScale * Vector3.up;
        rb.AddForce(gravityVector, ForceMode.Acceleration);             //constantly applies gravity to objects Rigidbody
    }

    public void setGravityScale(float gravityScale) {
        this.gravityScale = gravityScale;
    }
}
