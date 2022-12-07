using UnityEngine;
using System;

public class RayCastManipulator : MonoBehaviour
{
    public float ChangeHeightMagnitude;
    public LayerMask LayerMskForFloor;
    public LimitsData LocalLimits;
    public LimitsData WorldLimits;
    public enum WhichWay {up, down};
    public float _hitDistance { private set; get; }

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private WhichWay _way;
    private GeneratingPlane PlaneData;

    [Serializable]
    public struct LimitsData
    {
        public float LimitMaxY;
        public float LimitMinY;

        public LimitsData(float limitMaxY, float limitMinY)
        {
            LimitMaxY = limitMaxY;
            LimitMinY = limitMinY;
        }
    }

    private void FixedUpdate()
    {
        DrawTriangles();
    }

    public void ChangeHeightPlane(WhichWay way)
    {
        RaycastHit hit;
        _way = way;

        if (Physics.Raycast(transform.position, -transform.up, out hit, 3f, LayerMskForFloor))
        {
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return;

            mesh = meshCollider.sharedMesh;
            vertices = mesh.vertices;
            triangles = mesh.triangles;
            PlaneData = hit.collider.gameObject.GetComponent<GeneratingPlane>();
            _hitDistance = hit.distance;

            float tmpChangeHeightMagnitude = ChangeHeightMagnitude;
            switch (_way)
            {
                case WhichWay.up:
                    tmpChangeHeightMagnitude = ChangeHeightMagnitude;
                    break;
                case WhichWay.down:
                    tmpChangeHeightMagnitude = -ChangeHeightMagnitude;
                    break;
            }

            float tmpPlanePosY = hit.collider.gameObject.transform.position.y;
            WorldLimits = new LimitsData(tmpPlanePosY + LocalLimits.LimitMaxY, tmpPlanePosY - LocalLimits.LimitMinY);

            ChangeHeightVertices(hit.triangleIndex, tmpChangeHeightMagnitude);

            meshCollider.convex = true;
            meshCollider.convex = false;
        }
    }

    private void ChangeHeightVertices(int triangleIndex, float changeHeightMagnitude)
    {
        Vector3 p0 = vertices[triangles[triangleIndex * 3 + 0]];
        Vector3 p1 = vertices[triangles[triangleIndex * 3 + 1]];
        Vector3 p2 = vertices[triangles[triangleIndex * 3 + 2]];

        SetPosVertex(triangles[triangleIndex * 3 + 0], p0, changeHeightMagnitude);
        SetPosVertex(triangles[triangleIndex * 3 + 1], p1, changeHeightMagnitude);
        SetPosVertex(triangles[triangleIndex * 3 + 2], p2, changeHeightMagnitude);

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    private bool IsOutOfLimit(float currentHeight)
    {
        if(_way == WhichWay.up)
        {
            return (Mathf.Abs(LocalLimits.LimitMaxY - currentHeight)) < ChangeHeightMagnitude;
        }
        else
        {
            return (Mathf.Abs(LocalLimits.LimitMinY - currentHeight)) < ChangeHeightMagnitude;
        }
    }

    private void SetPosVertex(int vertexIndex, Vector3 posVertex, float changeHeightMagnitude)
    {
        if (!IsOutOfLimit(posVertex.y) && !PlaneData.IsVertexIgnore(vertexIndex))
        {
            vertices[vertexIndex] = new Vector3(posVertex.x, posVertex.y + changeHeightMagnitude, posVertex.z);
        }
    }

    private void DrawTriangles()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, 3f, LayerMskForFloor))
        {
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return;

            mesh = meshCollider.sharedMesh;
            vertices = mesh.vertices;
            triangles = mesh.triangles;

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
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, -transform.up * 3f);
    }
}