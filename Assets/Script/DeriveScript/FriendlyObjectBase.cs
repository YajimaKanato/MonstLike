using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class FriendlyObjectBase : MonoBehaviour
{
    [Header("CoolTime")]
    [SerializeField]
    float _coolTime = 1;

    [Header("Power")]
    [SerializeField]
    float _power = 1;

    [Header("Bullet")]
    [SerializeField]
    protected GameObject _bullet;

    Rigidbody2D _rb2d;

    Vector3 _basePos;

    bool _isAttacking = false;
    float _delta = 0;
    protected static float _simulateSpeed = 1;
    public static float SimulateSpeed { set { _simulateSpeed = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0;
        _rb2d.freezeRotation = true;
        _basePos = transform.position;
        gameObject.tag = "FriendlyObject";
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _basePos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!_isAttacking)
            {
                FriendAttack();
                StartCoroutine(CoolTimeCoroutine());
            }
        }
    }

    /// <summary>
    /// クールタイムを管理する関数
    /// </summary>
    /// <returns></returns>
    IEnumerator CoolTimeCoroutine()
    {
        _isAttacking = true;
        while (true)
        {
            _delta += Time.deltaTime;
            if (_delta > _coolTime)
            {
                _delta = 0;
                _isAttacking = false;
                yield break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// プレイヤーが当たった時に行う関数
    /// </summary>
    protected abstract void FriendAttack();
}
