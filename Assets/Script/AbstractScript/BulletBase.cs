using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BulletBase : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField]
    protected float _speed = 10;

    [Header("LifeTime")]
    [SerializeField]
    protected float _lifeTime = 3;

    [Header("Power")]
    [SerializeField]
    float _power = 1;

    [Header("ExplosionParticle")]
    [SerializeField]
    GameObject _particle;

    protected Rigidbody2D _rb2d;

    protected float _delta = 0;
    protected float _degree;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetUp();
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

    public float GetPower()
    {
        return _power;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(_particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    /// <summary>
    /// ‰Šúİ’è‚·‚éŠÖ”
    /// </summary>
    protected abstract void SetUp();
}
