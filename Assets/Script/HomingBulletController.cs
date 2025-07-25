using System.Collections;
using UnityEngine;

public class HomingBulletController : BulletBase
{
    GameObject[] _target;
    GameObject _returnTarget;
    GameObject _getTarget;

    float _compareHP;
    bool _firstActionEnd = false;

    protected override void SetUp()
    {
        _getTarget = GetTarget();
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0;
        gameObject.tag = "FriendlyBullet";
        if (_getTarget)
        {
            StartCoroutine(FirstCoroutine());
        }
        else
        {
            _rb2d.linearVelocity = transform.right * _speed * BulletBase._simulateSpeed;
        }
    }

    private void Update()
    {
        _delta += Time.deltaTime;
        if (_delta > _lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (_firstActionEnd)
        {
            if (_getTarget)
            {
                _rb2d.linearVelocity = (_getTarget.transform.position - gameObject.transform.position).normalized * _speed * BulletBase._simulateSpeed;
            }
            else
            {
                _getTarget = GetTarget();
            }
        }
    }

    /// <summary>
    /// ターゲットとなるGameObjectを返す関数
    /// </summary>
    /// <returns></returns>
    GameObject GetTarget()
    {
        _target = GameObject.FindGameObjectsWithTag("Enemy");
        _returnTarget = null;
        _compareHP = 0;

        if (_target.Length <= 0)
        {
            return null;
        }
        else
        {
            foreach (GameObject go in _target)
            {
                if (go.GetComponent<EnemyBase>().GetHPRate() > _compareHP)
                {
                    _returnTarget = go;
                    _compareHP = go.GetComponent<EnemyBase>().GetHPRate();
                }
            }
        }
        return _returnTarget;
    }

    /// <summary>
    /// 最初のモーションを管理する関数
    /// </summary>
    /// <returns></returns>
    IEnumerator FirstCoroutine()
    {
        int direction = Random.Range(0, 2);
        int[] ints = { -1, 1 };
        while (true)
        {
            if (_getTarget)
            {
                //ターゲットとのベクトルに一定の角度まで近づいたら
                if (Vector3.Dot(_rb2d.linearVelocity.normalized, (_getTarget.transform.position - gameObject.transform.position).normalized) > Mathf.Cos(Mathf.PI / 12))
                {
                    _firstActionEnd = true;
                    yield break;
                }
                else
                {
                    //少し回転
                    gameObject.transform.rotation *= Quaternion.AngleAxis(1 * ints[direction], Vector3.forward);
                    _rb2d.linearVelocity = transform.right * _speed * BulletBase._simulateSpeed;
                    yield return null;
                }
            }
            else
            {
                yield break;
            }
        }
    }
}
