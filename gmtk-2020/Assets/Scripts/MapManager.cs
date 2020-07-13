using System.Collections.Generic;
using Toolbox;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(MeshFilter))]
public class MapManager : MonoBehaviour
{
    public Vector3Int bounds = new Vector3Int(16, 16, 16);
    public float perlinScale2D = 0.9f;
    public float perlinScale = 0.9f;
    public float perlinThreshold = 0.5f;

    int[,,] data;

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public void Build()
    {
        mesh = new Mesh();
        mesh.indexFormat = IndexFormat.UInt32;

        GetComponent<MeshFilter>().mesh = mesh;

        CreateData();
        CreateShape();
        UpdateMesh();
    }

    void CreateData()
    {
        data = new int[bounds.x, bounds.y, bounds.z];

        PerlinNoiseGrid3D noise3D = new PerlinNoiseGrid3D(bounds.x, bounds.y, bounds.z, perlinScale);
        Vector3 offset = new Vector3(Random.value * 100000, Random.value * 100000, Random.value * 100000);

        for (int k = 0; k < bounds.z; k++)
        {
            for (int j = 0; j < bounds.y; j++)
            {
                for (int i = 0; i < bounds.x; i++)
                {
                    float value3D = noise3D[i, j, k];

                    float nx = ((float)i / bounds.x) - 0.5f;
                    float nz = ((float)k / bounds.z) - 0.5f;
                    // float d = Mathf.Sqrt(nx * nx + nz * nz) / Mathf.Sqrt(0.5f);
                    float d = Mathf.Sqrt(nx * nx + nz * nz);

                    // value3D = (1 + value3D - d) / 2;

                    float value2D = Mathf.PerlinNoise(offset.x + ((float)i / bounds.x) * perlinScale2D, offset.z + ((float)k / bounds.z) * perlinScale2D);
                    value2D = (0.05f + (value2D * 0.95f)) * bounds.y;

                    if (d > 0.4f)
                    {
                        // value2D -= ((d - 0.4f) / 0.1f) * bounds.y;
                        value2D *= 1f - Mathf.Clamp01((d - 0.45f) / 0.05f);
                    }

                    if (j < value2D && value3D >= perlinThreshold)
                    // if (j < value2D && d < 0.5f)
                    // if (j < value2D)
                    {
                        data[i, j, k] = 1;
                    }
                }
            }
        }
    }

    void CreateShape()
    {
        vertices = new Vector3[bounds.x * bounds.y * bounds.z * 6 * 4];

        List<int> tris = new List<int>();

        int index = 0;

        for (int k = 0; k < bounds.z; k++)
        {
            for (int j = 0; j < bounds.y; j++)
            {
                for (int i = 0; i < bounds.x; i++)
                {
                    if (data[i, j, k] != 0)
                    {
                        Vector3 backBottomLeft = new Vector3(i, j, k);
                        Vector3 backBottomRight = new Vector3(i + 1, j, k);
                        Vector3 backTopLeft = new Vector3(i, j + 1, k);
                        Vector3 backTopRight = new Vector3(i + 1, j + 1, k);
                        Vector3 frontBottomLeft = new Vector3(i, j, k + 1);
                        Vector3 frontBottomRight = new Vector3(i + 1, j, k + 1);
                        Vector3 frontTopLeft = new Vector3(i, j + 1, k + 1);
                        Vector3 frontTopRight = new Vector3(i + 1, j + 1, k + 1);

                        // -z face
                        vertices[index] = backBottomLeft;
                        vertices[index + 1] = backBottomRight;
                        vertices[index + 2] = backTopLeft;
                        vertices[index + 3] = backTopRight;

                        if ((k - 1) < 0 || data[i, j, k - 1] == 0)
                        {
                            tris.Add(index);
                            tris.Add(index + 2);
                            tris.Add(index + 1);
                            tris.Add(index + 2);
                            tris.Add(index + 3);
                            tris.Add(index + 1);
                        }

                        index += 4;

                        // +z face
                        vertices[index] = frontBottomRight;
                        vertices[index + 1] = frontBottomLeft;
                        vertices[index + 2] = frontTopRight;
                        vertices[index + 3] = frontTopLeft;

                        if ((k + 1) >= bounds.z || data[i, j, k + 1] == 0)
                        {
                            tris.Add(index);
                            tris.Add(index + 2);
                            tris.Add(index + 1);
                            tris.Add(index + 2);
                            tris.Add(index + 3);
                            tris.Add(index + 1);
                        }

                        index += 4;

                        // -y face
                        vertices[index] = frontBottomLeft;
                        vertices[index + 1] = frontBottomRight;
                        vertices[index + 2] = backBottomLeft;
                        vertices[index + 3] = backBottomRight;

                        if ((j - 1) < 0 || data[i, j - 1, k] == 0)
                        {
                            tris.Add(index);
                            tris.Add(index + 2);
                            tris.Add(index + 1);
                            tris.Add(index + 2);
                            tris.Add(index + 3);
                            tris.Add(index + 1);
                        }

                        index += 4;

                        // +y face
                        vertices[index] = backTopLeft;
                        vertices[index + 1] = backTopRight;
                        vertices[index + 2] = frontTopLeft;
                        vertices[index + 3] = frontTopRight;

                        if ((j + 1) >= bounds.y || data[i, j + 1, k] == 0)
                        {
                            tris.Add(index);
                            tris.Add(index + 2);
                            tris.Add(index + 1);
                            tris.Add(index + 2);
                            tris.Add(index + 3);
                            tris.Add(index + 1);
                        }

                        index += 4;

                        // -x face
                        vertices[index] = frontBottomLeft;
                        vertices[index + 1] = backBottomLeft;
                        vertices[index + 2] = frontTopLeft;
                        vertices[index + 3] = backTopLeft;

                        if ((i - 1) < 0 || data[i - 1, j, k] == 0)
                        {
                            tris.Add(index);
                            tris.Add(index + 2);
                            tris.Add(index + 1);
                            tris.Add(index + 2);
                            tris.Add(index + 3);
                            tris.Add(index + 1);
                        }

                        index += 4;

                        // +x face
                        vertices[index] = backBottomRight;
                        vertices[index + 1] = frontBottomRight;
                        vertices[index + 2] = backTopRight;
                        vertices[index + 3] = frontTopRight;

                        if ((i + 1) >= bounds.x || data[i + 1, j, k] == 0)
                        {
                            tris.Add(index);
                            tris.Add(index + 2);
                            tris.Add(index + 1);
                            tris.Add(index + 2);
                            tris.Add(index + 3);
                            tris.Add(index + 1);
                        }

                        index += 4;
                    }
                }
            }

            triangles = tris.ToArray();
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
