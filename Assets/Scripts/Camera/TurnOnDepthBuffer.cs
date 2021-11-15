using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
     Script that allows the Camera to write into its Depth Buffer
     This can Buffer can be accessed from other Scripts and Shader
     The highlight shader for intersection Highlights works based on the Depts Buffer
*/

public class TurnOnDepthBuffer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.DepthNormals;
    }
}
