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

    Vector3 _shotVector = Vector3.zero;//初速

    bool _isShotNow = false;
    bool _isAttacking = false;
    protected int _combo;
    float _nowHP;
    static float _simulateSpeed = 1;//スローモーションとかに使う
    public static float SimulateSpeed { set { _simulateSpeed = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Rigidbody2Dの初期設定
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0;
        _rb2d.sharedMaterial.friction = _friction;
        _rb2d.sharedMaterial.bounciness = _bounciness;
        //タグ取得
        gameObject.tag = "Enemy";
        //プレイヤー取得
        _player = GameObject.FindWithTag("Player");
        _nowHP = _hp;
        _charge.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //打ち出し
        if (!_isShotNow)
        {
            if (_player == null)
            {
                Debug.LogWarning("Playerが見つかりません");
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

        //次の打ち出しまでのインターバル
        if (_isShotNow)
        {
            _delta += Time.deltaTime * EnemyBase._simulateSpeed;
            if (_delta > _interval)
            {
                _delta = 0;
                _isShotNow = false;
            }
        }

        //死亡処理
        if (_hp <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        //減速
        _shotVector *= Mathf.Pow(_deceleration, EnemyBase._simulateSpeed);

        _rb2d.linearVelocity = _shotVector * EnemyBase._simulateSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //プレイヤーだけが攻撃中だったら
            if (_player.GetComponent<PlayerShot>().GetState() && !_isAttacking)
            {
                //ダメージを受ける
                Debug.Log("<color=red>E</color>:Damage!");
                Damage();
            }
            else if (!_player.GetComponent<PlayerShot>().GetState() && _isAttacking)
            {
                //攻撃アクションをする
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
    /// 攻撃力を取得する関数
    /// </summary>
    /// <returns></returns>
    public float GetAttackPower()
    {
        return _attackPower;
    }

    /// <summary>
    /// 攻撃中かどうかを取得する関数
    /// </summary>
    /// <returns></returns>
    public bool GetState()
    {
        return _isAttacking;
    }

    /// <summary>
    /// 現在HPを取得する関数
    /// </summary>
    /// <returns></returns>
    public float GetHPRate()
    {
        return _nowHP / _hp;
    }

    /// <summary>
    /// 攻撃する関数
    /// </summary>
    public abstract float Attack();

    /// <summary>
    /// ダメージを受けた時のアクションを制御する関数
    /// </summary>
    public abstract void Damage();

    /// <summary>
    /// 死ぬときのアクションをする関数
    /// </summary>
    public abstract void Die();
}
