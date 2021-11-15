using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchObjectCollision : MonoBehaviour
{
    [SerializeField]
    FruitNinjaController fruitNinjaController;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tags.splittalbe)
            fruitNinjaController.increaseScore(collision.gameObject);
    }
}
