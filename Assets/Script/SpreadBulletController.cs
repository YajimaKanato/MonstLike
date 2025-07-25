using UnityEngine;


public class SpreadBulletController : BulletBase
{
    float _degree;

    protected override void SetUp()
    {
        _degree = transform.rotation.eulerAngles.z;
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0;
        gameObject.tag = "FriendlyBullet";
    }

    private void FixedUpdate()
    {
        //_rb2d.linearVelocity = new Vector3(Mathf.Cos(_degree * Mathf.Deg2Rad), Mathf.Sin(_degree * Mathf.Deg2Rad), 0) * _speed * BulletBase._simulateSpeed;
        _rb2d.linearVelocity = transform.right * _speed * BulletBase._simulateSpeed;
    }
}
