using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllsMechanicScene : MonoBehaviour
{
    /// <summary>
    /// Class that takes in Player Inputs and implies them within the given logic
    /// </summary>

    [SerializeField]
    GameObject cutGameObjectContainer, cuttingPlane;
    
    [SerializeField]
    GameObject[] cutableObjects;

    [SerializeField]
    SplittingPlane splittingPlane;

    [SerializeField]
    float rotationSpeedPlane, rotationSpeedCube;

    bool cutButtonPressed = false, fire1ButtonPressed = false, fire2ButtonPressed = false, gravityEnabled = false;

    int activeObjectIndex;
    
    void Update()
    {
        if (!gravityEnabled && cutButtonPressed == false && Input.GetAxis("Jump") > .8)
        {
            cutCube();
            //remove after recording
            enableGravity();
            cutButtonPressed = true;
        }
        else if(!gravityEnabled && cutButtonPressed == true && Input.GetAxis("Jump") < .2)
        {
            cutButtonPressed = false;
        }

        if (!gravityEnabled && fire1ButtonPressed == false && Input.GetAxis("Fire1") > .8)
        {
            nextMesh();
            fire1ButtonPressed = true;
        }
        else if (!gravityEnabled && fire1ButtonPressed == true && Input.GetAxis("Fire1") < .2)
        {
            fire1ButtonPressed = false;
        }

        if (!gravityEnabled && fire2ButtonPressed == false && Input.GetAxis("Fire2") > .8)
        {
            enableGravity();
            fire2ButtonPressed = gravityEnabled = true;
        }
        else if (!gravityEnabled && fire2ButtonPressed == true && Input.GetAxis("Fire2") < .2)
        {
            fire2ButtonPressed = false;
        }
        else if (gravityEnabled && fire2ButtonPressed == false && Input.GetAxis("Fire2") > .8)
        {
            resetCamera();
            fire2ButtonPressed = true;
            gravityEnabled = false;
        }
        else if (gravityEnabled && fire2ButtonPressed == true && Input.GetAxis("Fire2") < .2)
        {
            fire2ButtonPressed = false;
        }

        if (Input.GetAxis("Menu") > .8)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void FixedUpdate()
    {
        if (!gravityEnabled && Input.GetAxis("Pivot") != 0)
            cuttingPlane.transform.localRotation *= Quaternion.Euler(Vector3.forward * Input.GetAxis("Pivot") * rotationSpeedPlane);

        if (!gravityEnabled && Input.GetAxis("Horizontal") != 0)
            cutGameObjectContainer.transform.localRotation *= Quaternion.Euler(-Vector3.up * Input.GetAxis("Horizontal") * rotationSpeedCube);

        if (!gravityEnabled && Input.GetAxis("Vertical") != 0)
            cutGameObjectContainer.transform.localRotation *= Quaternion.Euler(-Vector3.left * Input.GetAxis("Vertical") * rotationSpeedCube);
    }

    void cutCube() {
        splittingPlane.cut();
    }

    void nextMesh()
    {
        cutGameObjectContainer.transform.rotation = Quaternion.identity;

        splittingPlane.clearCollidingSplittables();
        foreach (Transform trans in cutGameObjectContainer.GetComponentsInChildren<Transform>()) {
            if (trans != cutGameObjectContainer.transform)
            {
                Destroy(trans.gameObject);
            }
        }

        activeObjectIndex = (activeObjectIndex+1)%cutableObjects.Length;

        GameObject go = Instantiate(cutableObjects[activeObjectIndex], cutGameObjectContainer.transform.position, Quaternion.identity);
        go.transform.parent = cutGameObjectContainer.transform;
    }

    void enableGravity() {

        foreach (Transform trans in cutGameObjectContainer.GetComponentsInChildren<Transform>())
        {
            if (trans != cutGameObjectContainer.transform)
            {
                trans.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                trans.GetComponent<Rigidbody>().useGravity = true;
            }
        }

        Camera.main.GetComponent<Animation>().Play("CameraTiltDownwards");
    }

    void resetCamera() {
        Camera.main.GetComponent<Animation>().Play("CameraTiltUpwards");

        nextMesh();
    }
}
