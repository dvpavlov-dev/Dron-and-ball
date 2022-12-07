using UnityEngine;

public class GoalZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            GameManager.Instance.Win();
        }
    }
}
