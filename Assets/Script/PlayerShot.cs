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
        //引っ張り
        if (Input.GetMouseButtonDown(0) && !_isShotNow)
        {
            //マウス座標を取得しワールド座標に変換
            _mousePos = Input.mousePosition;
            _mouseStart = Camera.main.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, 10));
        }
        if (Input.GetMouseButtonUp(0) && !_isShotNow)
        {
            //マウス座標を取得しワールド座標に変換
            _mousePos = Input.mousePosition;
            _mouseEnd = Camera.main.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, 10));
            //打ち出すベクトルを取得
            _speed = _mouseStart - _mouseEnd;
            _speed = _speed / _speed.magnitude;
            //取得したベクトルで打ち出す
            _rb2d.AddForce(_speed * _maxSpeed, ForceMode2D.Impulse);
            _isShotNow = true;
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
        if (_rb2d.linearVelocity.magnitude > 2f)
        {
            _rb2d.linearVelocity *= _deceleration;
        }
        else
        {
            _isShotNow = false;
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
            //当たった後に敵の速度よりも速くなっていたら（敵より遅く当たったら）
            if (enemy.GetComponent<Rigidbody2D>().linearVelocity.magnitude < _rb2d.linearVelocity.magnitude)
            {
                //ダメージを受ける
                Damage(enemy);
            }
            else
            {
                //攻撃アクションをする
                Attack();
            }
        }
    }
}
