using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyBase : MonoBehaviour
{
    [Header("DecelerationRate")]
    [SerializeField]
    float _deceleration;

    [Header("Speed")]
    [SerializeField]
    float _Speed;

    Rigidbody2D _rb2d;
    GameObject _player;

    bool _isShotNow = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _player = GameObject.FindWithTag("Player");
        if (_player == null)
        {
            Debug.LogWarning("Playerが見つかりません");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isShotNow)
        {
            _rb2d.linearVelocity = (_player.transform.position - transform.position).normalized * _Speed;
            _isShotNow = true;
        }
    }

    private void FixedUpdate()
    {
        if (_rb2d.linearVelocity.magnitude > 1f)
        {
            _rb2d.linearVelocity *= _deceleration;
        }
        else
        {
            _isShotNow = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //プレイヤーの速度よりも速かったら（当たった後）
            if (_player.GetComponent<Rigidbody2D>().linearVelocity.magnitude > _rb2d.linearVelocity.magnitude)
            {
                PlayerHitAction();
            }
        }
    }

    /// <summary>
    /// プレイヤーが当たってきたときに行う関数
    /// </summary>
    protected abstract void PlayerHitAction();
}
