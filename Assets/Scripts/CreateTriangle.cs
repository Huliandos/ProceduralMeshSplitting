using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTriangle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<MeshFilter>()) {
            MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
            gameObject.AddComponent<MeshRenderer>();

            Mesh mesh = new Mesh();

            mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0) , new Vector3(1, 0, 0) };
            mesh.triangles = new int[] { 0, 1, 2};
            mesh.normals = new Vector3[] { new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1)};
            mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0) };

            meshFilter.mesh = mesh;
        }

        Plane plane = new Plane(Vector3.up, Vector3.zero);
        

        MeshCollider col = gameObject.AddComponent<MeshCollider>();
        col.convex = true;

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;

        rb.constraints = RigidbodyConstraints.FreezeAll;

        gameObject.AddComponent<SplittableInfo>();
        gameObject.AddComponent<DisplayMeshData>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
