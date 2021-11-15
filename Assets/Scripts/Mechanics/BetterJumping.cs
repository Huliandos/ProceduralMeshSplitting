using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Script that enables a better feeling of jumping
    Two things:
    Jumping should have a quick and low and a long and high jump allowing the player more control
    Then falling should feel faster than rising
    This is all done by changing the gravity scale depending on Inputs and the player rising or falling
    Kyle Pittman's GDC talk https://www.youtube.com/watch?v=hG9SzQxaCm8
*/

public class BetterJumping : MonoBehaviour
{
    //Variables can be edited in Inspector
    //Values to be multiplied with gravity in case of low jump/falling
    [SerializeField]
    float fallMultiplier = 2.5f, lowJumpMultiplier = 2f;

    //reference to this objects custom Gravity script to affect its Gravity scale. 3D Rigidbodies in Unity don't offer that feature
    CustomGravity customGravity;
    Rigidbody rb;

    void Start()
    {
        customGravity = GetComponent<CustomGravity>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.y < 0)                                                      //if player is falling downwards
            customGravity.setGravityScale(fallMultiplier);
        else if (rb.velocity.y > 0 && Input.GetAxis("Jump") == 0)                   //if player is rising upwards (when jumping) but released the Jump Button
            customGravity.setGravityScale(lowJumpMultiplier);
        else                                                                        //if player isn't falling or jumping
            customGravity.setGravityScale(1);
    }
}
