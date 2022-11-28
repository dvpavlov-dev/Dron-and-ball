using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Drawing;

//[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GeneratingPlane : MonoBehaviour
{
    public int Size = 0;
    [SerializeField] private Material _material;
    [SerializeField] private float _offcetPosVertices;

    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;

    private MeshFilter _meshFilter;
    private MeshRenderer _meshRender;
    private Vector2[] _uvs;

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
        _meshRender = gameObject.AddComponent<MeshRenderer>();

        _meshFilter.mesh = _mesh;
        _meshRender.material = _material;
        GeneratePlane();
    }

    private void GeneratePlane()
    {
        _vertices = new Vector3[(Size + 1) * (Size + 1)];
        _triangles = new int[Size * Size * 6];
        _uvs = new Vector2[_vertices.Length];

        float y = transform.position.y;

        for (int z = 0, i = 0; z <= Size; z++)
        {
            for (int x = 0; x <= Size; x++, i++)
            {
                _vertices[i] = new Vector3(x * _offcetPosVertices, y, z * _offcetPosVertices);
                _uvs[i] = new Vector2((float)x/Size, (float)y / Size);
            }
        }

        //Создание треугольников
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
    }

    void UpdateMesh()
    {
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.uv = _uvs;
        _mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (_vertices != null && _vertices.Length != 0) 
        {
            for (int i = 0; i < _vertices.Length; i++)
            {
                Gizmos.DrawWireSphere(_vertices[i], 0.1f);
            }
                
        }
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

