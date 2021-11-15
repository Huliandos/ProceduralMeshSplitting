using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittableInfo: MonoBehaviour
{
    /// <summary>
    ///This class is used to control cutting behaviour of an Object
    ///Not attaching it to an Object will use the default values of cap = true, concave = true, cutMaterial = false
    /// </summary>
    

    [SerializeField] [Tooltip("Enable or disable caping the inside of a Mesh")]
    bool cap = false;

    [SerializeField] [Tooltip("Set concave flag to true if object is concave. Concave cutting operations are slower than convex ones")]
    bool concave = false;

    [SerializeField] [Tooltip("Set meshSeperation flag to true to seperate not connected Meshs after cut")]
    bool meshSeperation = false;

    [SerializeField] [Tooltip("Sets fill Material for caping. Leaving this at null will use the Meshs first Material")]
    Material cutMaterial = null;

    public bool getCap() {
        return cap;
    }

    public void setCap(bool cap)
    {
        this.cap = cap;
    }

    public bool getConcave() {
        return concave;
    }

    public void setConcave(bool concave)
    {
        this.concave = concave;
    }

    public bool getMeshSeperation() {
        return meshSeperation;
    }

    public void setMeshSeperation(bool meshSeperation)
    {
        this.meshSeperation = meshSeperation;
    }

    public Material getCutMaterial() {
        return cutMaterial;
    }

    public void setCutMaterial(Material cutMaterial)
    {
        this.cutMaterial = cutMaterial;
    }
}
