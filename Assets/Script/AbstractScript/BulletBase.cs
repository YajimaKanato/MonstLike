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
        if (_particle)
        {
            Instantiate(_particle, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("爆発のパーティクルが設定されていません");
        }
            Destroy(gameObject);
    }

    /// <summary>
    /// 初期設定する関数
    /// </summary>
    protected abstract void SetUp();
}
