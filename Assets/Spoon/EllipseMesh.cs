using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipseMesh : MonoBehaviour
{
    public float xRadius = 2f / 3f;
    public float yRadius = 1f;
    public float innerRadius = 0f;
    public float outerRadius = 1f;
    public float delay;
    public float speed = 3f;
    public Color color;

    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;

    void Start()
    {
        mesh = new Mesh();

        MeshFilter mf = GetComponent<MeshFilter>();
        mf.mesh = mesh;

        vertices = new List<Vector3>();
        vertices.Add(new Vector3(innerRadius, 0, 0));
        vertices.Add(new Vector3(outerRadius, 0, 0));

        triangles = new List<int>();

        Material m = GetComponent<MeshRenderer>().material;
        m.color = color;
    }

    float maxAngle = (Mathf.PI * 2f);
    float lastT;

    void Update()
    {
        if (lastT < maxAngle)
        {
            int i = vertices.Count - 2;

            float t = Mathf.Clamp((Time.time - delay) * speed, 0f, maxAngle);

            Vector3 p1 = new Vector3(Mathf.Cos(t) * xRadius, Mathf.Sin(t) * yRadius, 0.0f) * innerRadius;
            vertices.Add(p1);

            Vector3 p2 = new Vector3(Mathf.Cos(t) * xRadius, Mathf.Sin(t) * yRadius, 0.0f) * outerRadius;
            vertices.Add(p2);

            triangles.Add(i);
            triangles.Add(i + 2);
            triangles.Add(i + 1);

            triangles.Add(i + 2);
            triangles.Add(i + 3);
            triangles.Add(i + 1);

            mesh.Clear();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();

            lastT = t;
        }
    }
}
