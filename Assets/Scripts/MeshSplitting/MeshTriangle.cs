using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTriangle
{
    /// <summary>
    ///Helper class that stores a triangles three vertices, normals, uvs and the submesh Index of all three
    ///Used for easier Management and storge of the triangle data
    /// </summary>
    

    List<Vector3> vertices;
    List<Vector3> normals;
    List<Vector2> uvs;
    List<Vector2> rtLightmapUvs;
    int submeshIndex;

    public MeshTriangle(Vector3[] vertices, Vector3[] normals, Vector2[] uvs, Vector2[] rtLightmapUvs, int submeshIndex){ 
        this.vertices = new List<Vector3>();
        this.normals = new List<Vector3>();
        this.uvs = new List<Vector2>();
        this.rtLightmapUvs = new List<Vector2>();

        this.vertices.AddRange(vertices);
        this.normals.AddRange(normals);
        this.uvs.AddRange(uvs);
        this.rtLightmapUvs.AddRange(rtLightmapUvs);
        this.submeshIndex = submeshIndex;
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

    public int getSubmeshIndex() {
        return submeshIndex;
    }
    public void setSubmeshIndex(int submeshIndex) {
        this.submeshIndex = submeshIndex;
    }
    #endregion
}
