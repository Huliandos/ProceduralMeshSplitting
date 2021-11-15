using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedMesh
{
    /// <summary>
    ///Helper class that stores all the Mesh data for a Mesh that's about to be generated
    ///Different submeshes are all represented as different List<int> within the submeshIndices list
    ///All this data is then used to Instentiate a Unity Mesh
    /// </summary>
    

    List<Vector3> vertices = new List<Vector3>();
    List<Vector3> normals = new List<Vector3>();
    List<Vector2> uvs = new List<Vector2>();
    List<Vector2> rtLightmapUvs = new List<Vector2>();
    List<List<int>> submeshIndices = new List<List<int>>();

    public void addTriangle(MeshTriangle meshTriangle) {
        if (submeshIndices.Count < meshTriangle.getSubmeshIndex() + 1)
        {
            for (int i = submeshIndices.Count; i < meshTriangle.getSubmeshIndex() + 1; i++)
            {
                submeshIndices.Add(new List<int>());
            }
        }

        //save overhead by only adding vertex/normal combos that aren't in the List. Set vertexIndices accordingly
        for (int i = 0; i < meshTriangle.getVertices().Count; i++)
        {
            int indexOfDuplicate = vertices.IndexOf(meshTriangle.getVertices()[i]);

            if (indexOfDuplicate < 0 || normals[indexOfDuplicate] != meshTriangle.getNormals()[i])
            {
                submeshIndices[meshTriangle.getSubmeshIndex()].Add(vertices.Count);

                vertices.Add(meshTriangle.getVertices()[i]);
                normals.Add(meshTriangle.getNormals()[i]);
                uvs.Add(meshTriangle.getUvs()[i]);
                rtLightmapUvs.Add(meshTriangle.getRtLightmapUvs()[i]);
            }
            else
            {
                submeshIndices[meshTriangle.getSubmeshIndex()].Add(indexOfDuplicate);
            }
        }
    }

    #region getter/setter
    public List<Vector3> getVertices() {
        return vertices;
    }
    public void setVertices(List<Vector3> vertices) {
        this.vertices = vertices;
    }

    public List<Vector3> getNormals() {
        return normals;
    }
    public void setNormals(List<Vector3> normals) {
        this.normals = normals;
    }

    public List<Vector2> getUvs() {
        return uvs;
    }
    public void setUvs(List<Vector2> uvs) {
        this.uvs = uvs;
    }

    public List<Vector2> getRtLightmapUvs()
    {
        return rtLightmapUvs;
    }
    public void setRtLightmapUvs(List<Vector2> rtLightmapUvs)
    {
        this.rtLightmapUvs = rtLightmapUvs;
    }

    public List<List<int>> getSubmeshIndices() {
        return submeshIndices;
    }
    public void setSubmeshIndices(List<List<int>> submeshIndices) {
        this.submeshIndices = submeshIndices;
    }
    #endregion
}
