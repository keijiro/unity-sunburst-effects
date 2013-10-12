using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class SunburstMesh : MonoBehaviour
{
    #region Public variables
    public int beamCount = 90;
    public float beamWidth = 0.1f;
    public float speed = 0.4f;
    public float scalePower = 1.0f;
    #endregion

    #region Private variables
    Mesh mesh;
    Vector3[] tips;
    Vector3[] vertices;
    #endregion

    #region Monobehaviour functions
    void Awake ()
    {
        // Allocate the vertex array.
        vertices = new Vector3[beamCount * 3];

        // Initialize the tip positions and the normal vector array.
        tips = new Vector3[beamCount * 2];
        var normals = new Vector3[beamCount * 3];
        for (var i = 0; i < beamCount; i++) {
            var v = Random.onUnitSphere;
            var d = Random.onUnitSphere * beamWidth;
            var n = Vector3.Cross (v, d).normalized;

            tips [i * 2 + 0] = v - d;
            tips [i * 2 + 1] = v + d;

            normals [i * 3 + 0] = Vector3.Lerp (v, n, 0.5f).normalized;
            normals [i * 3 + 1] = n;
            normals [i * 3 + 2] = n;
        }

        // Initialize the triangle set.
        var indices = new int[beamCount * 3];
        for (var i = 0; i < beamCount * 3; i++) {
            indices [i] = i;
        }

        // Initialize the mesh.
        mesh = new Mesh ();
        mesh.MarkDynamic ();
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.SetTriangles (indices, 0);
        GetComponent<MeshFilter> ().sharedMesh = mesh;
    }

    void Update ()
    {
        var time = Time.time * speed;
        var noiseCoeff = 0.77f;

        for (var i = 0; i < beamCount; i++) {
            var scale = Mathf.Pow (Mathf.PerlinNoise (time, i * noiseCoeff), scalePower);
            vertices [3 * i + 1] = tips [2 * i + 0] * scale;
            vertices [3 * i + 2] = tips [2 * i + 1] * scale;
        }

        mesh.vertices = vertices;
    }
    #endregion
}