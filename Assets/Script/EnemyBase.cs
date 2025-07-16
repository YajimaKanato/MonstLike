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

    Rigidbody2D _rb2d;
    GameObject _player;

    bool _isShotNow = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        gameObject.tag = "Enemy";
        _player = GameObject.FindWithTag("Player");
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
                _rb2d.AddForce(new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * _speed, ForceMode2D.Impulse);
            }
            else
            {
                _rb2d.AddForce((_player.transform.position - transform.position).normalized * _speed, ForceMode2D.Impulse);
            }
            _isShotNow = true;
        }

        //次の打ち出しまでのインターバル
        if (_isShotNow)
        {
            _delta += Time.deltaTime;
            if (_delta > _interval)
            {
                _delta = 0;
                _isShotNow = false;
            }
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
        _rb2d.linearVelocity *= _deceleration;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = collision.gameObject;
            //当たった後にプレイヤーの速度よりも速くなっていたら（プレイヤーより遅く当たったら）
            if (player.GetComponent<Rigidbody2D>().linearVelocity.magnitude < _rb2d.linearVelocity.magnitude)
            {
                Debug.Log("E:Damage!");
                _hp -= player.GetComponent<PlayerShot>().Attack();
            }
        }
    }

    /// <summary>
    /// 攻撃する関数
    /// </summary>
    public abstract float Attack();
}
