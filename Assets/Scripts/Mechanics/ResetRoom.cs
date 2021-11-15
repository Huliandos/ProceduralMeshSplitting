using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRoom : MonoBehaviour
{
    [SerializeField]
    GameObject[] prefabsForReset;

    [SerializeField]
    GameObject resetContainer;

    List<Vector3> positions = new List<Vector3>();
    List<Quaternion> rotations = new List<Quaternion>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform objTrans in resetContainer.GetComponentsInChildren<Transform>()) {
            if (objTrans != resetContainer.transform)
            {
                positions.Add(objTrans.position);
                rotations.Add(objTrans.rotation);
            }
        }
    }

    public void resetRoom() {
        foreach (Transform objTrans in resetContainer.GetComponentsInChildren<Transform>())
        {
            if (objTrans != resetContainer.transform)
            {
                Destroy(objTrans.gameObject);
            }
        }

        for (int i = 0; i < prefabsForReset.Length; i++) {
            Instantiate(prefabsForReset[i], positions[i], rotations[i], resetContainer.transform);
        }
    }
}
