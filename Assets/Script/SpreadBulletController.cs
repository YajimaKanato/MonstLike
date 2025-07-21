using UnityEngine;


public class SpreadBulletController : BulletBase
{
    protected override void SetUp()
    {
        _degree = transform.rotation.eulerAngles.z;
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0;
        _rb2d.linearVelocity = new Vector3(Mathf.Cos(_degree * Mathf.Deg2Rad), Mathf.Sin(_degree * Mathf.Deg2Rad), 0) * _speed;
        gameObject.tag = "FriendlyBullet";
    }
}
