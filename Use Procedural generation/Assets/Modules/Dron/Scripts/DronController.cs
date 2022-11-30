using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DronController : MonoBehaviour
{
    public float Speed;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * Speed, 0, Input.GetAxis("Vertical") * Speed), ForceMode.Impulse);
    }
}
