using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMinigame : MonoBehaviour
{
    public void startMinigame() {
        GetComponent<FruitNinjaController>().startMinigame();
    }
}
