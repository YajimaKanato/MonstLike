using UnityEditor.ShaderGraph.Internal;
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

    Rigidbody2D _rb2d;
    protected GameObject _player;

    bool _isShotNow = false;
    protected int _combo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0;
        _rb2d.sharedMaterial.friction = _friction;
        _rb2d.sharedMaterial.bounciness = _bounciness;
        gameObject.tag = "Enemy";
        _player = GameObject.FindWithTag("Player");
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
                _rb2d.AddForce(new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * _speed, ForceMode2D.Impulse);
            }
            else
            {
                _rb2d.AddForce((_player.transform.position - transform.position).normalized * _speed, ForceMode2D.Impulse);
            }
            _isShotNow = true;
        }

        //���̑ł��o���܂ł̃C���^�[�o��
        if (_isShotNow)
        {
            _delta += Time.deltaTime;
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
        _rb2d.linearVelocity *= _deceleration;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //����������Ƀv���C���[�̑��x���������Ȃ��Ă�����i�v���C���[���x������������j
            if (_player.GetComponent<Rigidbody2D>().linearVelocity.magnitude < _rb2d.linearVelocity.magnitude)
            {
                //�_���[�W���󂯂�
                Debug.Log("<color=red>E</color>:Damage!");
                Damage();
            }
            else
            {
                //�U���A�N�V����������
                Attack();
            }
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
