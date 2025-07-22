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

    public static float _simulateSpeed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetUp();
        gameObject.tag = "FriendlyBullet";
    }

    // Update is called once per frame
    void Update()
    {
        _delta += Time.deltaTime * _simulateSpeed;
        if (_delta > _lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {

    }

    /// <summary>
    /// �e�̍U���͂��擾����֐�
    /// </summary>
    /// <returns></returns>
    public float GetPower()
    {
        return _power;
    }

    /// <summary>
    /// �V�~�����[�V�������x��ω�������֐�
    /// </summary>
    /// <param name="speed"> ���{�ɂ��邩�̐��l</param>
    /// <returns></returns>
    public void SpeedDown(float speed)
    {
        _simulateSpeed = speed;
        _rb2d.linearVelocity *= speed;
    }

    public void SpeedUp(float speed)
    {
        _simulateSpeed = 1;
        _rb2d.linearVelocity /= speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_particle)
        {
            Instantiate(_particle, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("�����̃p�[�e�B�N�����ݒ肳��Ă��܂���");
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// �����ݒ肷��֐�
    /// </summary>
    protected abstract void SetUp();
}
