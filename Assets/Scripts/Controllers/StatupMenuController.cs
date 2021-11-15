using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatupMenuController : MonoBehaviour
{
    public void loadMechanicScene(){
        SceneManager.LoadScene(1);
    }

    public void loadGameScene()
    {
        SceneManager.LoadScene(2);
    }
}
