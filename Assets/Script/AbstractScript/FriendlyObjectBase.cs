using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class FriendlyObjectBase : MonoBehaviour
{
    Rigidbody2D _rb2d;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0;
        Debug.Log("a");
    }

    // Update is called once per frame
    void Update()
    {
        _rb2d.linearVelocity = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FriendAttack();
        }
    }

    /// <summary>
    /// プレイヤーが当たった時に行う関数
    /// </summary>
    protected abstract void FriendAttack(); 
}
