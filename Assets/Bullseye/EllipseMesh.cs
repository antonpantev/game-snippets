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
    public float speed = 1f;
    public Color color;
    public int quadCount = 120;
    public float delayReverse;

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
    float reverseStart = Mathf.Infinity;

    void Update()
    {
        if (lastT < 1)
        {
            float t = Mathf.Clamp01((Time.time - delay) * speed);
            t = EasingFunction.EaseInOutSine(0, 1, t);

            int target = Mathf.FloorToInt(t * quadCount);

            while (vertices.Count - 2 < target)
            {
                int i = vertices.Count - 2;

                float angle = ((i + 2f) / quadCount) * maxAngle;

                Vector3 p1 = new Vector3(Mathf.Cos(angle) * xRadius, Mathf.Sin(angle) * yRadius, 0.0f) * innerRadius;
                vertices.Add(p1);

                Vector3 p2 = new Vector3(Mathf.Cos(angle) * xRadius, Mathf.Sin(angle) * yRadius, 0.0f) * outerRadius;
                vertices.Add(p2);

                triangles.Add(i);
                triangles.Add(i + 2);
                triangles.Add(i + 1);

                triangles.Add(i + 2);
                triangles.Add(i + 3);
                triangles.Add(i + 1);
            }

            mesh.Clear();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();

            lastT = t;
        }
        else if (reverseStart == Mathf.Infinity)
        {
            reverseStart = Time.time + delayReverse;
        }

        if (reverseStart < Time.time)
        {
            float t = Mathf.Clamp01((Time.time - reverseStart) * speed);
            t = EasingFunction.EaseInOutSine(1, 0, t);
            int target = Mathf.FloorToInt(t * quadCount) + 2;

            vertices.RemoveRange(target, vertices.Count - target);
            triangles.RemoveRange((target - 2) * 3, triangles.Count - ((target - 2) * 3));

            mesh.Clear();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
        }
    }
}
