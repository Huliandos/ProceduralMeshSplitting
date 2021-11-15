using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    /// <summary>
    /// Class that takes in Player Inputs and implies them within the given logic
    /// Also initializes cutting once the strike button has first been pressed and then released
    /// </summary>
    
    #region movement fields
    //Speed values for Players movement controlls
    [SerializeField]
    float speed = .15f, mouseSpeed = 2f, jumpSpeed = 40f, swordRotationSpeed = .5f;
    float swordRotationResetStepSize = .01f;

    float yaw, pitch;

    //Rigidbodies used to reference moveable character parts like the camera and the whole avatar
    Rigidbody rb, cameraRb;

    //References the sword of the player, used for its manipulation
    [SerializeField]
    GameObject sword, swordCrosshair;

    //booleans making sure that striking and jumping only works once they're finished
    bool striking, jumping, interactButtonPressed;
    #endregion

    //The script used for cutting
    [SerializeField]
    SplittingPlane splittingPlane;

    //References the gameController, a script used to monitor and control game states 
    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        cameraRb = Camera.main.GetComponent<Rigidbody>();

        gameController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();
    }

    private void Update()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            if (!striking)
            {
                sword.GetComponent<Animation>().Play("SwordRaise");
                swordCrosshair.SetActive(true);

                gameController.slowDownTime();
                striking = true;

                swordRotationResetStepSize = .01f;
            }
            /*
            if (Input.GetAxis("Pivot") != 0)
            {
                sword.transform.localEulerAngles += Vector3.forward * Input.GetAxis("Pivot") * swordRotationSpeed;
            }*/
        }
        else if (Input.GetAxis("Fire1") == 0 && striking)
        {
            //strike here
            //start mesh splitting operations here
            splittingPlane.cut();

            sword.GetComponent<Animation>().Play("SwordStrike");
            swordCrosshair.SetActive(false);

            gameController.resetTime();
            striking = false;
        }/*
        else if (sword.transform.localEulerAngles.z != 0 && !sword.GetComponent<Animation>().isPlaying)
        {
            if (sword.transform.localEulerAngles.z > 180)
                sword.transform.localEulerAngles = Vector3.Lerp(sword.transform.localEulerAngles, new Vector3(0, 0, 360), swordRotationResetStepSize);
            else
                sword.transform.localEulerAngles = Vector3.Lerp(sword.transform.localEulerAngles, Vector3.zero, swordRotationResetStepSize);

            swordRotationResetStepSize += .01f;

            if (swordRotationResetStepSize > 1)
            {
                swordRotationResetStepSize = .01f;

                sword.transform.localEulerAngles = Vector3.zero;
            }
        }*/

        yaw += mouseSpeed * Input.GetAxis("Mouse X");
        pitch -= mouseSpeed * Input.GetAxis("Mouse Y");

        cameraRb.transform.eulerAngles = new Vector3(pitch, yaw, 0);

        /*
        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)      //if mouse if moved around
            cameraRb.MoveRotation(cameraRb.rotation * Quaternion.Euler(new Vector3(clipRotation(-Input.GetAxis("Mouse Y") * mouseSpeed), Input.GetAxis("Mouse X") * mouseSpeed, -cameraRb.gameObject.transform.eulerAngles.z)));
            */
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            rb.MovePosition(gameObject.transform.position + (cameraRb.gameObject.transform.forward * Input.GetAxis("Vertical") * speed)
                + (cameraRb.gameObject.transform.right * Input.GetAxis("Horizontal") * speed));
        

        if (Input.GetAxis("Interact") > .8f && !interactButtonPressed) {
            interactButtonPressed = true;

            RaycastHit hit;
            Physics.Raycast(cameraRb.transform.position, cameraRb.transform.forward, out hit, 2);

            if (hit.collider && hit.collider.tag == Tags.button)
            {
                if (hit.collider.GetComponent<ResetRoom>())
                    hit.collider.GetComponent<ResetRoom>().resetRoom();
                else
                    hit.collider.GetComponent<StartMinigame>().startMinigame();
            }
        }
        else if (Input.GetAxis("Interact") < .2f && interactButtonPressed) {
            interactButtonPressed = false;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            if (Input.GetAxis("Pivot") != 0)
            {
                sword.transform.localEulerAngles += Vector3.forward * Input.GetAxis("Pivot") * swordRotationSpeed;
            }
        }
        else if (sword.transform.localEulerAngles.z != 0 && !sword.GetComponent<Animation>().isPlaying)
        {
            if (sword.transform.localEulerAngles.z > 180)
                sword.transform.localEulerAngles = Vector3.Lerp(sword.transform.localEulerAngles, new Vector3(0, 0, 360), swordRotationResetStepSize);
            else
                sword.transform.localEulerAngles = Vector3.Lerp(sword.transform.localEulerAngles, Vector3.zero, swordRotationResetStepSize);

            swordRotationResetStepSize += .01f;

            if (swordRotationResetStepSize > 1)
            {
                swordRotationResetStepSize = .01f;

                sword.transform.localEulerAngles = Vector3.zero;
            }
        }

        if (Input.GetAxis("Jump") > 0 && !jumping)
        {
            //add jumping off walls and enemies possible later
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            jumping = true;
        }
    }

    float clipRotation(float rot)
    {
        if (rot > 0 && rb.transform.eulerAngles.x > 80 && rb.transform.eulerAngles.x < 180)     //if camera angle exceeds 80 degrees
        {
            return 0;
        }
        else if (rot < 0 && rb.transform.eulerAngles.x < 280 && rb.transform.eulerAngles.x > 180)       //if camera angle gets smaller than -80 degrees
        {
            return 0;
        }
        else
        {
            return rot;
        }
    }

    public void setSpeed(float speed, float mouseSpeed) {
        this.speed = speed;
        this.mouseSpeed = mouseSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        jumping = false;
    }
}
