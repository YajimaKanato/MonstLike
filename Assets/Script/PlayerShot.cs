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

    [Header("MaxSpeed")]
    [SerializeField]
    float _maxSpeed;

    Rigidbody2D _rb2d;
    PhysicsMaterial2D _material;

    Vector3 _mouseStart;
    Vector3 _mouseEnd;
    Vector3 _mousePos;
    Vector3 _speed;

    bool _isShotNow = false;

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
        if (Input.GetMouseButtonDown(0) && !_isShotNow)
        {
            //�}�E�X���W���擾�����[���h���W�ɕϊ�
            _mousePos = Input.mousePosition;
            _mouseStart = Camera.main.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, 10));
        }
        if (Input.GetMouseButtonUp(0) && !_isShotNow)
        {
            //�}�E�X���W���擾�����[���h���W�ɕϊ�
            _mousePos = Input.mousePosition;
            _mouseEnd = Camera.main.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, 10));
            //�ł��o���x�N�g�����擾
            _speed = _mouseStart - _mouseEnd;
            _speed = _speed / _speed.magnitude;
            //�擾�����x�N�g���őł��o��
            _rb2d.AddForce(_speed * _maxSpeed, ForceMode2D.Impulse);
            _isShotNow = true;
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
        if (_rb2d.linearVelocity.magnitude > 1f)
        {
            _rb2d.linearVelocity *= _deceleration;
        }
        else
        {
            _isShotNow = false;
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

    //�U�����ɍs���A�N�V����������֐�
    void Attack()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject enemy = collision.gameObject;
            //����������ɓG�̑��x���������Ȃ��Ă�����i�G���x������������j
            if (enemy.GetComponent<Rigidbody2D>().linearVelocity.magnitude < _rb2d.linearVelocity.magnitude)
            {
                //�_���[�W���󂯂�
                Debug.Log("P:Damage!");
                _hp -= enemy.GetComponent<EnemyBase>().GetAttackPower();
            }
            else
            {
                //�U���A�N�V����������
                Attack();
            }
        }
    }
}
