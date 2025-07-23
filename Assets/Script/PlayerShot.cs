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

    [Header("NextAttackSpeed"), Tooltip("次の攻撃に移れる速度")]
    [SerializeField]
    float _nextAttackSpeed = 3;

    [Header("Charge")]
    [SerializeField]
    GameObject _charge;

    Rigidbody2D _rb2d;
    PhysicsMaterial2D _material;

    Vector3 _mouseStart;
    Vector3 _mouseEnd;
    Vector3 _mousePos;
    Vector3 _vector;

    bool _isAttacking = false;
    bool _isDragging = false;

    static float _simulateSpeed = 1;
    public static float SimulateSpeed { set { _simulateSpeed = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0;
        _material = GetComponent<PhysicsMaterial2D>();
        _charge.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //引っ張り
        if (Input.GetMouseButtonDown(0) && !_isAttacking)
        {
            //マウス座標を取得しワールド座標に変換
            _mousePos = Input.mousePosition;
            _mouseStart = Camera.main.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, 10));
            _charge?.SetActive(true);
            _isDragging = true;
        }
        if (Input.GetMouseButtonUp(0) && !_isAttacking && _isDragging)
        {
            //マウス座標を取得しワールド座標に変換
            _mousePos = Input.mousePosition;
            _mouseEnd = Camera.main.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, 10));
            //打ち出すベクトルを取得
            _vector = _mouseStart - _mouseEnd;
            _vector = _vector / _vector.magnitude;
            //取得したベクトルで打ち出す
            _rb2d.AddForce(_vector * _speed, ForceMode2D.Impulse);
            _rb2d.linearVelocity = _vector * _speed * _simulateSpeed;
            _isAttacking = true;
            _charge?.SetActive(false);
            _isDragging = false;
        }

        //死亡処理
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        //減速
        if (_rb2d.linearVelocity.magnitude > _nextAttackSpeed * _simulateSpeed)
        {
            _vector *= _deceleration;
            _rb2d.linearVelocity = _vector * _speed * _simulateSpeed;
        }
        else
        {
            _isAttacking = false;
        }
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
    /// 攻撃力を取得する関数
    /// </summary>
    /// <returns></returns>
    public float GetAttackPower()
    {
        return _attackPower;
    }

    /// <summary>
    /// 攻撃時に行うアクションをする関数
    /// </summary>
    void Attack()
    {

    }

    /// <summary>
    /// ダメージを受けた時の関数
    /// </summary>
    /// <param name="enemy"> ダメージを与えてきたオブジェクト</param>
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
            //敵だけが攻撃中だったら
            if (enemy.GetComponent<EnemyBase>().GetState() && !_isAttacking)
            {
                //ダメージを受ける
                Damage(enemy);
            }
            else if (!enemy.GetComponent<EnemyBase>().GetState() && _isAttacking)
            {
                //攻撃アクションをする
                Attack();
            }
        }
    }
}
