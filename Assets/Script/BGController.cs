using UnityEngine;

public class BGController : MonoBehaviour
{
    [Header("MovingObject"), Tooltip("操作するオブジェクト（プレイヤーなど）を設定してください")]
    [SerializeField]
    GameObject _movingObject;

    [Header("SpriteRenderer"), Tooltip("背景のスプライトを設定してください")]
    [SerializeField]
    SpriteRenderer _spriteRenderer;

    [Header("BackGround"), Tooltip("動かす背景オブジェクト")]
    [SerializeField]
    GameObject _backGround;

    [Header("MoveRate"), Tooltip("操作するオブジェクトに対してどれくらいの速度で動かすか")]
    [SerializeField]
    float _moveRate;

    Rigidbody2D _movingObjectRb2d;
    Rigidbody2D _rb2d;

    Vector3 _basePos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!_movingObject)
        {
            Debug.LogWarning("オブジェクトが設定されていません");
        }
        else
        {
            _movingObjectRb2d = _movingObject.GetComponent<Rigidbody2D>();
            if (_movingObjectRb2d)
            {
                //Rigidbodyで管理
                _backGround.AddComponent<Rigidbody2D>();
                _rb2d = _backGround.GetComponent<Rigidbody2D>();
                _rb2d.gravityScale = 0;
            }
            else
            {
                Debug.Log("動かすオブジェクトにはRigidbodyがありません");
                _basePos = _backGround.transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //操作するオブジェクトにRigidbodyがあるかどうかで動き方を変える
        if (_movingObjectRb2d)
        {
            _rb2d.linearVelocity = _movingObjectRb2d.linearVelocity * _moveRate;
        }
        else
        {
            _backGround.transform.position += (_movingObject.transform.position - _basePos) * _moveRate;
        }

        if (Mathf.Abs(_movingObject.transform.position.x - _backGround.transform.position.x) >= _spriteRenderer.bounds.size.x ||
            Mathf.Abs(_movingObject.transform.position.y - _backGround.transform.position.y) >= _spriteRenderer.bounds.size.y)
        {//動かすオブジェクトがSpriteのサイズの分だけ動いたら座標調整
            _backGround.transform.position = _movingObject.transform.position;
        }
    }
}
