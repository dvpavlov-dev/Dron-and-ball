using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

public class GeneratingPlane : MonoBehaviour
{
    public int Size = 0;
    public Gradient ColorHeight;
    public MeshRenderer MeshRender { get; private set; }

    private List<int> _trianglesIgnore = new List<int>();

    [SerializeField] private Material _material;
    [SerializeField] private float _offcetPosVertices;

    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;
    private MeshFilter _meshFilter;
    private Vector2[] _uvs;
    Color[] ColorLevels;

    private void Start()
    {
        InitParameters();
    }

    [Button ("Generate plane")]
    public void InitParameters()
    {
        if(GetComponent<MeshFilter>() != null)
        {
            DestroyImmediate(GetComponent<MeshFilter>());
        }

        if (GetComponent<MeshRenderer>() != null)
        {
            DestroyImmediate(GetComponent<MeshRenderer>());
        }

        if (GetComponent<MeshCollider>() != null)
        {
            DestroyImmediate(GetComponent<MeshCollider>());
        }

        _mesh = new Mesh();

        _meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRender = gameObject.AddComponent<MeshRenderer>();

        _meshFilter.mesh = _mesh;
        MeshRender.material = _material;
        GeneratePlane();
    }

    public bool IsVertexIgnore(int vertex)
    {
        foreach(int triangle in _trianglesIgnore)
        {
            int vertex1 = _triangles[triangle * 3 + 0];
            int vertex2 = _triangles[triangle * 3 + 1];
            int vertex3 = _triangles[triangle * 3 + 2];
            if(vertex == vertex1 || vertex == vertex2 || vertex == vertex3)
            {
                return true;
            }
        }
        return false;
    }

    private void GeneratePlane()
    {
        _vertices = new Vector3[(Size + 1) * (Size + 1)];
        _triangles = new int[Size * Size * 6];
        _uvs = new Vector2[_vertices.Length];

        float y = 0;

        for (int z = 0, i = 0; z <= Size; z++)
        {
            for (int x = 0; x <= Size; x++, i++)
            {
                _vertices[i] = new Vector3(x * _offcetPosVertices, y, z * _offcetPosVertices);
                _uvs[i] = new Vector2((float)x/Size, (float)y / Size);
            }
        }

        DrawMesh();

        for (int z = 0, triangleIndex = 0, VertIndex = 0; z < Size; z++, VertIndex++)
        {
            for (int x = 0; x < Size; x++, triangleIndex+=6, VertIndex++)
            {
                _triangles[0 + triangleIndex] = VertIndex;
                _triangles[1 + triangleIndex] = VertIndex + Size + 1;
                _triangles[2 + triangleIndex] = VertIndex + 1;
                _triangles[3 + triangleIndex] = VertIndex + 1;
                _triangles[4 + triangleIndex] = VertIndex + Size + 1;
                _triangles[5 + triangleIndex] = VertIndex + Size + 2;
                UpdateMesh();
            }
        }
        gameObject.AddComponent<MeshCollider>();
        CreateIgnoreVertices(Size);
    }

    private void UpdateMesh()
    {
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.uv = _uvs;
        _mesh.RecalculateNormals();
    }

    private void CreateIgnoreVertices(int size)
    {
        _trianglesIgnore.Clear();

        for (int i = 0; i < size * size * 2; i++)
        {
            if(i <= size * 2 - 1)
            {
                _trianglesIgnore.Add(i);
            }
            else if (i >= ((size * size * 2 - 1) - (size * 2 - 1)))
            {
                _trianglesIgnore.Add(i);
            }
            else if (i == (Mathf.Round(i / (size * 2)) * size * 2) 
                || i == (Mathf.Round(i / (size * 2)) * size * 2) + 1
                || i == (Mathf.Round(i / (size * 2)) * size * 2) + size * 2 - 2
                || i == (Mathf.Round(i / (size * 2)) * size * 2) + size * 2 - 1)
            {
                _trianglesIgnore.Add(i);
            }
        }
    }

    void DrawMesh()
    {
        ColorLevels = new Color[_vertices.Length];
        _uvs = new Vector2[_vertices.Length];
        for (int v = 0; v < _vertices.Length; v++)
        {
            ColorLevels[v] = ColorHeight.Evaluate(0.5f);
        }

        Texture2D tx2d = new Texture2D(Size, Size);
        for (int x = 0, i = 0; x <= Size; x++)
        {
            for (int y = 0; y <= Size; y++)
            {
                _uvs[i] = new Vector2((float)x / Size, (float)y / Size);
                tx2d.SetPixel(x, y, ColorLevels[i]);
                i++;
            }
        }
        tx2d.Apply();
        MeshRender.sharedMaterial.mainTexture = tx2d;
    }

    private void TestPlane()
    {
        _vertices = new Vector3[4];
        _triangles = new int[6];

        _vertices[0] = new Vector3(0, 0, 0);
        _vertices[1] = new Vector3(1, 0, 0);
        _vertices[2] = new Vector3(0, 0, 1);
        _vertices[3] = new Vector3(1, 0, 1);

        _triangles[0] = 0;
        _triangles[1] = 2;
        _triangles[2] = 1;
        _triangles[3] = 1;
        _triangles[4] = 2;
        _triangles[5] = 3;
    }
}

