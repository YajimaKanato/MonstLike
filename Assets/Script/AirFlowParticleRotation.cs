using UnityEngine;

public class AirFlowParticleRotation : MonoBehaviour
{
    [Header("Parent")]
    [SerializeField]
    GameObject _parent;

    Rigidbody2D _rb2d;

    Vector3 _vector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = _parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _vector = _rb2d.linearVelocity;
        gameObject.transform.right = -1 * _vector;
    }
}
