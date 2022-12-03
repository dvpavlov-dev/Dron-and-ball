using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DronController : MonoBehaviour
{
    public float Speed;
    private RayCastManipulator _rayCastManipulator;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rayCastManipulator = GetComponent<RayCastManipulator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_rb != null)
        {
            _rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * Speed, 0, Input.GetAxis("Vertical") * Speed), ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            _rayCastManipulator.ChangeHeightPlane(RayCastManipulator.WhichWay.up);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            _rayCastManipulator.ChangeHeightPlane(RayCastManipulator.WhichWay.down);
        }
    }
}
