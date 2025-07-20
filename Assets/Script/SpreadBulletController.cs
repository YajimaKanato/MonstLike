using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpreadBulletController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField]
    float _speed = 10;

    [Header("LifeTime")]
    [SerializeField]
    float _lifeTime = 3;

    Rigidbody2D _rb2d;

    float _delta = 0;
    float _degree;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _degree = transform.rotation.eulerAngles.z;
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0;
        _rb2d.linearVelocity = new Vector3(Mathf.Cos(_degree * Mathf.Deg2Rad), Mathf.Sin(_degree * Mathf.Deg2Rad), 0) * _speed;
        Debug.Log(_rb2d.linearVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        _delta += Time.deltaTime;
        if (_delta > _lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
