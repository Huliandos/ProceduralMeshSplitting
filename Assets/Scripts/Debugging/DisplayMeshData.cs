using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMeshData : MonoBehaviour
{
#if UNITY_EDITOR 
    public Vector3[] vertices;
    public Vector3[] normals;
    public Vector2[] uvs;
    public Vector2[] normalUvs;
    public List<int> submeshIndices;

    // Start is called before the first frame update
    void Start()
    {
        vertices = GetComponent<MeshFilter>().mesh.vertices;
        normals = GetComponent<MeshFilter>().mesh.normals;
        uvs = GetComponent<MeshFilter>().mesh.uv;
        normalUvs = GetComponent<MeshFilter>().mesh.uv2;

        submeshIndices = new List<int>();
        for (int i = 0; i < GetComponent<MeshFilter>().mesh.subMeshCount; i++) {
            //To have a clear seperator as to where a new submesh layer starts
            submeshIndices.Add(-1);
            submeshIndices.Add(i);
            submeshIndices.Add(-1);

            for (int j = 0; j < GetComponent<MeshFilter>().mesh.GetTriangles(i).Length; j++)
                submeshIndices.Add(GetComponent<MeshFilter>().mesh.GetTriangles(i)[j]);
        }
    }
#endif
}
