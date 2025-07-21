using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerShot : MonoBehaviour
{
    [Header("HP")]
    [SerializeField]
    float _hp;

    [Header("AttackPower")]
    [SerializeField]
    float _attackPower;

    [Header("DecelerationRate")]
    [SerializeField]
    float _deceleration;

    [Header("Speed")]
    [SerializeField]
    float _speed;

    [Header("NextAttackSpeed"), Tooltip("���̍U���Ɉڂ�鑬�x")]
    [SerializeField]
    float _nextAttackSpeed = 3;

    Rigidbody2D _rb2d;
    PhysicsMaterial2D _material;

    Vector3 _mouseStart;
    Vector3 _mouseEnd;
    Vector3 _mousePos;
    Vector3 _vector;

    bool _isAttacking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0;
        _material = GetComponent<PhysicsMaterial2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //��������
        if (Input.GetMouseButtonDown(0) && !_isAttacking)
        {
            //�}�E�X���W���擾�����[���h���W�ɕϊ�
            _mousePos = Input.mousePosition;
            _mouseStart = Camera.main.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, 10));
        }
        if (Input.GetMouseButtonUp(0) && !_isAttacking)
        {
            //�}�E�X���W���擾�����[���h���W�ɕϊ�
            _mousePos = Input.mousePosition;
            _mouseEnd = Camera.main.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, 10));
            //�ł��o���x�N�g�����擾
            _vector = _mouseStart - _mouseEnd;
            _vector = _vector / _vector.magnitude;
            //�擾�����x�N�g���őł��o��
            _rb2d.AddForce(_vector * _speed, ForceMode2D.Impulse);
            _isAttacking = true;
        }

        //���S����
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        //����
        if (_rb2d.linearVelocity.magnitude > _nextAttackSpeed)
        {
            _rb2d.linearVelocity *= _deceleration;
        }
        else
        {
            _isAttacking = false;
        }
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
    /// �U���͂��擾����֐�
    /// </summary>
    /// <returns></returns>
    public float GetAttackPower()
    {
        return _attackPower;
    }

    /// <summary>
    /// �U�����ɍs���A�N�V����������֐�
    /// </summary>
    void Attack()
    {

    }

    /// <summary>
    /// �_���[�W���󂯂����̊֐�
    /// </summary>
    /// <param name="enemy"> �_���[�W��^���Ă����I�u�W�F�N�g</param>
    void Damage(GameObject enemy)
    {
        Debug.Log("<color=yellow>P</color>:Damage!");
        _hp -= enemy.GetComponent<EnemyBase>().GetAttackPower();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject enemy = collision.gameObject;
            //�G�������U������������
            if (enemy.GetComponent<EnemyBase>().GetState() && !_isAttacking)
            {
                //�_���[�W���󂯂�
                Damage(enemy);
            }
            else if(!enemy.GetComponent<EnemyBase>().GetState() && _isAttacking)
            {
                //�U���A�N�V����������
                Attack();
            }
        }
    }
}
