using System.Linq;
using UnityEngine;

public class CheckCompleteEngulfment : MonoBehaviour
{
    //Using the basic idea of the Ray casting/crossing number/even-odd rule algorithm
    public bool isEngulfedInCollider(MeshCollider meshColliderToCheck, Vector3[] raycastPoints, int numberOfRaycastsPerSide)
    {
        Vector3 raycastDirection;
        float raycastLength;
        RaycastHit hit;
        Ray ray;
        int hitCounter = 0;
        
        for (int i = 0; i < raycastPoints.Length; i++) {
            raycastPoints[i] = transform.position + transform.TransformDirection(raycastPoints[i]);
        }

        for (int i = 0; i < numberOfRaycastsPerSide; i++) {
            ///BOTTOM LEFT TO BOTTOM RIGHT\\\
            //raycast from bottom left to top right, while turning to bottom right to top left from iteration to iteration
            hitCounter = 0;
            
            //Debug.Log("TEST: " + raycastPoints[0]  + " " + (transform.right + transform.forward) * -1.3f + " " + transform.TransformDirection(new Vector3(-1.3f, 0, -1.3f)));

            raycastDirection = (raycastPoints[1] * (1f - (float)i / numberOfRaycastsPerSide) + raycastPoints[3] * ((float)i / numberOfRaycastsPerSide))
                - (raycastPoints[0] * (1f - (float)i / numberOfRaycastsPerSide) + raycastPoints[2] * ((float)i / numberOfRaycastsPerSide));
            raycastLength = raycastDirection.magnitude; //Find a way to calculate Edge point of Collider

            ray = new Ray(raycastPoints[0] * (1f - (float)i / numberOfRaycastsPerSide) + raycastPoints[2] * ((float)i / numberOfRaycastsPerSide), raycastDirection);

            //Debug.DrawRay(raycastPoints[0] * (1f - (float)i / numberOfRaycastsPerSide) + raycastPoints[2] * ((float)i / numberOfRaycastsPerSide), raycastDirection, Color.green);
            //Debug.Log(raycastDirection);

            //Debug.DrawRay(raycastPoints[0] * (1f - (float)i / numberOfRaycastsPerSide) + raycastPoints[2] * ((float)i / numberOfRaycastsPerSide) + raycastDirection, -raycastDirection, Color.red);

            if (meshColliderToCheck.Raycast(ray, out hit, raycastLength)) 
                hitCounter++;

            ray = new Ray(raycastPoints[0] * (1f - (float)i / numberOfRaycastsPerSide) + raycastPoints[2] * ((float)i / numberOfRaycastsPerSide) + raycastDirection, -raycastDirection);
            if (meshColliderToCheck.Raycast(ray, out hit, raycastLength))
                hitCounter++;
            
            if (hitCounter % 2 != 0)
                return false;

            ///BOTTOM RIGHT TO TOP RIGHT\\\
            hitCounter = 0;
            

            raycastDirection = (raycastPoints[3] * (1f - (float)i / numberOfRaycastsPerSide) + raycastPoints[0] * ((float)i / numberOfRaycastsPerSide))
                - (raycastPoints[2] * (1f - (float)i / numberOfRaycastsPerSide) + raycastPoints[1] * ((float)i / numberOfRaycastsPerSide));
            raycastLength = raycastDirection.magnitude; //Find a way to calculate Edge point of Collider

            ray = new Ray(raycastPoints[2] * (1f - (float)i / numberOfRaycastsPerSide) + raycastPoints[1] * ((float)i / numberOfRaycastsPerSide), raycastDirection);

            //Debug.DrawRay(raycastPoints[2] * (1f - (float)i / numberOfRaycastsPerSide) + raycastPoints[1] * ((float)i / numberOfRaycastsPerSide), raycastDirection, Color.green);
            //Debug.Log(raycastDirection);

            //Debug.DrawRay(raycastPoints[2] * (1f - (float)i / numberOfRaycastsPerSide) + raycastPoints[1] * ((float)i / numberOfRaycastsPerSide) + raycastDirection, -raycastDirection, Color.red);

            if (meshColliderToCheck.Raycast(ray, out hit, raycastLength))
                hitCounter++;

            ray = new Ray(raycastPoints[2] * (1f - (float)i / numberOfRaycastsPerSide) + raycastPoints[1] * ((float)i / numberOfRaycastsPerSide) + raycastDirection, -raycastDirection);
            if (meshColliderToCheck.Raycast(ray, out hit, raycastLength))
                hitCounter++;
            
            if (hitCounter % 2 != 0)
                return false;
        }
        return (true);
    }
}