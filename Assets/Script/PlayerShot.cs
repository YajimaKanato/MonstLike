using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerShot : MonoBehaviour
{
    [Header("DecelerationRate")]
    [SerializeField]
    float _deceleration;

    [Header("MaxDrag")]
    [SerializeField]
    float _maxDrag;

    [Header("MaxSpeed")]
    [SerializeField]
    float _maxSpeed;

    Rigidbody2D _rigid2d;
    PhysicsMaterial2D _material;

    Vector3 _mouseStart;
    Vector3 _mouseEnd;
    Vector3 _mousePos;
    Vector3 _speed;

    bool _isShotNow = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid2d = GetComponent<Rigidbody2D>();
        _material = GetComponent<PhysicsMaterial2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //引っ張り
        if (Input.GetMouseButtonDown(0) && !_isShotNow)
        {
            _mousePos = Input.mousePosition;
            _mouseStart = Camera.main.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, 10));
        }
        if (Input.GetMouseButtonUp(0) && !_isShotNow)
        {
            _mousePos = Input.mousePosition;
            _mouseEnd = Camera.main.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y, 10));
            _speed = _mouseStart - _mouseEnd;
            if (Vector3.Distance(_mouseStart, _mouseEnd) > _maxDrag)
            {
                _speed = _speed / _speed.magnitude * _maxDrag;
            }
            _rigid2d.linearVelocity = _speed * _maxSpeed;
            _isShotNow = true;
        }
    }

    private void FixedUpdate()
    {
        //減速
        if (_rigid2d.linearVelocity.magnitude > 1f)
        {
            _rigid2d.linearVelocity *= _deceleration;
        }
        else
        {
            _isShotNow = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //プレイヤーの速度よりも速かったら（当たった後）
            if (GameObject.FindWithTag("Enemy").GetComponent<Rigidbody2D>().linearVelocity.magnitude > _rigid2d.linearVelocity.magnitude)
            {

            }
        }
    }
}
