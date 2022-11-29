using UnityEngine;

public class TestRayCast : MonoBehaviour
{
    public Camera cam;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, -transform.up * 0.1f, Color.red);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, 10f))
        {
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return;

            mesh = meshCollider.sharedMesh;
            vertices = mesh.vertices;
            triangles = mesh.triangles;

            if(hit.triangleIndex % 2 == 0 || hit.triangleIndex == 0)
            {
                ChangeTriangleVertices(hit.triangleIndex);
                ChangeTriangleVertices(hit.triangleIndex + 1);
            }
            else
            {
                ChangeTriangleVertices(hit.triangleIndex);
                ChangeTriangleVertices(hit.triangleIndex - 1);
            }
            
        }
    }

    private void ChangeTriangleVertices(int triangleIndex)
    {
        DrawTriangles(triangleIndex);

        Vector3 p0 = vertices[triangles[triangleIndex * 3 + 0]];
        Vector3 p1 = vertices[triangles[triangleIndex * 3 + 1]];
        Vector3 p2 = vertices[triangles[triangleIndex * 3 + 2]];

        vertices[triangles[triangleIndex * 3 + 0]] = new Vector3(p0.x, p0.y + 0.0005f, p0.z);
        vertices[triangles[triangleIndex * 3 + 1]] = new Vector3(p1.x, p1.y + 0.0005f, p1.z);
        vertices[triangles[triangleIndex * 3 + 2]] = new Vector3(p2.x, p2.y + 0.0005f, p2.z);

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    private void DrawTriangles(int triangleIndex)
    {
        Vector3 p0 = vertices[triangles[triangleIndex * 3 + 0]];
        Vector3 p1 = vertices[triangles[triangleIndex * 3 + 1]];
        Vector3 p2 = vertices[triangles[triangleIndex * 3 + 2]];

        Debug.DrawLine(p0, p1);
        Debug.DrawLine(p1, p2);
        Debug.DrawLine(p2, p0);
    }
}
