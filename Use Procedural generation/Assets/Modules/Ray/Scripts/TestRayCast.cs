using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestRayCast : MonoBehaviour
{
    public Camera cam;

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, -transform.up * 10, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10f))
        {
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return;

            Mesh mesh = meshCollider.sharedMesh;
            Vector3[] vertices = mesh.vertices;
            int[] triangles = mesh.triangles;

            Vector3 p0 = vertices[triangles[hit.triangleIndex * 3 + 0]];
            Vector3 p1 = vertices[triangles[hit.triangleIndex * 3 + 1]];
            Vector3 p2 = vertices[triangles[hit.triangleIndex * 3 + 2]];
            Transform hitTransform = hit.collider.transform;
            p0 = hitTransform.TransformPoint(p0);
            p1 = hitTransform.TransformPoint(p1);
            p2 = hitTransform.TransformPoint(p2);
            Debug.DrawLine(p0, p1);
            Debug.DrawLine(p1, p2);
            Debug.DrawLine(p2, p0);

            vertices[triangles[hit.triangleIndex * 3 + 0]] = new Vector3(p0.x, p0.y + 0.001f, p0.z);
            vertices[triangles[hit.triangleIndex * 3 + 1]] = new Vector3(p1.x, p1.y + 0.001f, p1.z);
            vertices[triangles[hit.triangleIndex * 3 + 2]] = new Vector3(p2.x, p2.y + 0.001f, p2.z);

            mesh.vertices = vertices;
            mesh.RecalculateNormals();
        }
    }
}
