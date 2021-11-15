using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneForThesis
{
    Vector3 planeNormal;
    Vector3 pointInNormal;

    //Unity's implementation of Plane has a few more constructors to constuct Planes with two Vectors, Three Points, etc.
    public PlaneForThesis(Vector3 planeNormal, Vector3 pointInNormal) {
        this.planeNormal = planeNormal.normalized;
        this.pointInNormal = pointInNormal;
    }

    public bool GetSide(Vector3 point)
    {
        float num = Vector3.Dot(planeNormal, point);

        if (num > 0)
            return true;
        return false;
    }
}
