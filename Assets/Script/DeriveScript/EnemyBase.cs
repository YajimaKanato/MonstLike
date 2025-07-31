using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyBase : MonoBehaviour
{
    [Header("HP")]
    [SerializeField]
    protected float _hp;

    [Header("AttackPower")]
    [SerializeField]
    protected float _attackPower;

    [Header("NextAttackSpeed")]
    [SerializeField]
    float _nextAttackSpeed;

    [Header("DecelerationRate")]
    [SerializeField]
    protected float _deceleration;

    [Header("Speed")]
    [SerializeField]
    protected float _speed;

    [Header("ShotInterval")]
    [SerializeField]
    protected float _interval;
    float _delta;

    [Header("Rigidbody2D")]
    [SerializeField, Range(0, 1)]
    protected float _friction;
    [SerializeField, Range(0, 1)]
    protected float _bounciness;

    [Header("Particle")]
    [SerializeField]
    protected GameObject _particle;

    [Header("ChargeParticle")]
    [SerializeField]
    GameObject _charge;

    Rigidbody2D _rb2d;
    protected GameObject _player;

    Vector3 _shotVector = Vector3.zero;//����

    bool _isShotNow = false;
    bool _isAttacking = false;
    protected int _combo;
    float _nowHP;
    static float _simulateSpeed = 1;//�X���[���[�V�����Ƃ��Ɏg��
    public static float SimulateSpeed { set { _simulateSpeed = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Rigidbody2D�̏����ݒ�
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0;
        _rb2d.sharedMaterial.friction = _friction;
        _rb2d.sharedMaterial.bounciness = _bounciness;
        //�^�O�擾
        gameObject.tag = "Enemy";
        //�v���C���[�擾
        _player = GameObject.FindWithTag("Player");
        _nowHP = _hp;
        _charge.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //�ł��o��
        if (!_isShotNow)
        {
            if (_player == null)
            {
                Debug.LogWarning("Player��������܂���");
                float rad = Random.Range(0, 2 * Mathf.PI);
                //_rb2d.AddForce(new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * _speed * EnemyBase._simulateSpeed, ForceMode2D.Impulse);
                _shotVector = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * _speed;
                _rb2d.linearVelocity = _shotVector;
            }
            else
            {
                //_rb2d.AddForce((_player.transform.position - transform.position).normalized * _speed * EnemyBase._simulateSpeed, ForceMode2D.Impulse);
                _shotVector = (_player.transform.position - transform.position).normalized * _speed;
                _rb2d.linearVelocity = _shotVector * EnemyBase._simulateSpeed;
                _isAttacking = true;
            }
            _isShotNow = true;
            _charge?.SetActive(false);
        }

        if (_rb2d.linearVelocity.magnitude <= _nextAttackSpeed * EnemyBase._simulateSpeed)
        {
            _isAttacking = false;
            _charge?.SetActive(true);
        }

        //���̑ł��o���܂ł̃C���^�[�o��
        if (_isShotNow)
        {
            _delta += Time.deltaTime * EnemyBase._simulateSpeed;
            if (_delta > _interval)
            {
                _delta = 0;
                _isShotNow = false;
            }
        }

        //���S����
        if (_hp <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        //����
        _shotVector *= Mathf.Pow(_deceleration, EnemyBase._simulateSpeed);

        _rb2d.linearVelocity = _shotVector * EnemyBase._simulateSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //�v���C���[�������U������������
            if (_player.GetComponent<PlayerShot>().GetState() && !_isAttacking)
            {
                //�_���[�W���󂯂�
                Debug.Log("<color=red>E</color>:Damage!");
                Damage();
            }
            else if (!_player.GetComponent<PlayerShot>().GetState() && _isAttacking)
            {
                //�U���A�N�V����������
                Attack();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FriendlyBullet")
        {
            _hp -= collision.gameObject.GetComponent<BulletBase>().GetPower();
        }
    }

    /// <summary>
    /// �U���͂��擾����֐�
    /// </summary>
    /// <returns></returns>
    public float GetAttackPower()
    {
        return _attackPower;
    }

    /// <summary>
    /// �U�������ǂ������擾����֐�
    /// </summary>
    /// <returns></returns>
    public bool GetState()
    {
        return _isAttacking;
    }

    /// <summary>
    /// ����HP���擾����֐�
    /// </summary>
    /// <returns></returns>
    public float GetHPRate()
    {
        return _nowHP / _hp;
    }

    /// <summary>
    /// �U������֐�
    /// </summary>
    public abstract float Attack();

    /// <summary>
    /// �_���[�W���󂯂����̃A�N�V�����𐧌䂷��֐�
    /// </summary>
    public abstract void Damage();

    /// <summary>
    /// ���ʂƂ��̃A�N�V����������֐�
    /// </summary>
    public abstract void Die();
}
