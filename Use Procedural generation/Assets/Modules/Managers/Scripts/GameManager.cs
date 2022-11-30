using UnityEngine;
using NaughtyAttributes;

public class GameManager : MonoBehaviour
{
    public GameObject[] ProcedureFloors;

    [Button ("Reset vertices")]
    public void ResetVertices()
    {
        ProcedureFloors = GameObject.FindGameObjectsWithTag("Procedure Floor");
        foreach (GameObject Floor in ProcedureFloors)
        {
            Mesh mesh = Floor.GetComponent<MeshCollider>().sharedMesh;
            Vector3[] vertices = new Vector3[mesh.vertices.Length];

            for(int i = 0; i < mesh.vertices.Length; i++)
            {
                vertices[i] = new Vector3(0, 0, 0);
            }
            mesh.vertices = vertices;
            mesh.RecalculateNormals();
        }
    }
}
