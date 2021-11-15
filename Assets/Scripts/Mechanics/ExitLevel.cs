using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.player)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
