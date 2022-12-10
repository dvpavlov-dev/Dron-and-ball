using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DronController : MonoBehaviour
{
    public float Speed;
    public GameObject[] Effects;
    public GameObject FireLineUp;
    public GameObject[] FireEffectComponent;
    public GameObject FlashLineUp;
    public GameObject[] FlashEffectComponent;

    private RayCastManipulator _rayCastManipulator;
    private Rigidbody _rb;
    private AudioSource _audioSource;
    private bool _isDone;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _rayCastManipulator = GetComponent<RayCastManipulator>();
    }

    void FixedUpdate()
    {
        if(_rb != null)
        {
            _rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * Speed, 0, Input.GetAxis("Vertical") * Speed), ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            OnClick_Mouse0();
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            OnClick_Mouse1();
        }

        if(!Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
        {
            OnOffEffectsAndSounds(false, "");
        }
    }

    private void OnClick_Mouse0()
    {
        _rayCastManipulator.ChangeHeightPlane(RayCastManipulator.WhichWay.up);
        HeightConversion(_rayCastManipulator._hitDistance, FlashLineUp, FlashEffectComponent);
        OnOffEffectsAndSounds(true, "Laser bombardment4");
    }

    private void OnClick_Mouse1()
    {
        _rayCastManipulator.ChangeHeightPlane(RayCastManipulator.WhichWay.down);
        HeightConversion(_rayCastManipulator._hitDistance, FireLineUp, FireEffectComponent);
        OnOffEffectsAndSounds(true, "Laser bombardment3");
    }

    private void OnOffEffectsAndSounds(bool isActive, string effect)
    {
        if (isActive && _isDone == false)
        {
            ChooseEffect(effect);
            _audioSource.Play();
            _isDone = true;
        }
        if(!isActive && _isDone == true)
        {
            ChooseEffect(null);
            _audioSource.Stop();
            _isDone = false;
        }
    }

    private void ChooseEffect(string effect)
    {
        foreach(GameObject tmpEffect in Effects)
        {
            if(tmpEffect.name == effect)
            {
                tmpEffect.SetActive(true);
            }
            else
            {
                tmpEffect.SetActive(false);
            }
        }
    }

    private void HeightConversion(float height, GameObject lineUp, GameObject[] effectComponents)
    {
        float tmpDeltaPercent = 100 * height / 1.86f; // 1.86 The initial distance between the drone and the plane
        float tmpHeightLineUp = 0.1f * tmpDeltaPercent / 100f;
        lineUp.transform.localScale = new Vector3(FireLineUp.transform.localScale.x, tmpHeightLineUp, FireLineUp.transform.localScale.z);

        foreach(GameObject tmpEffect in effectComponents)
        {
            tmpEffect.transform.localPosition = new Vector3(tmpEffect.transform.localPosition.x, 1.90f - height, tmpEffect.transform.localPosition.z); ;
        }
    }
}
