using System.Collections;
using UnityEngine;

public class HomingBulletController : BulletBase
{
    GameObject[] _target;
    GameObject _returnTarget;
    GameObject _getTarget;

    float _firstAngle;
    float _compareHP;
    bool _firstActionEnd = false;

    protected override void SetUp()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0;
        gameObject.tag = "FriendlyBullet";
        _firstAngle = transform.rotation.eulerAngles.z;
        StartCoroutine(FirstCoroutine());
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
            _getTarget = GetTarget();
            if (_getTarget)
            {
                _rb2d.linearVelocity = (GetTarget().transform.position - gameObject.transform.position).normalized * _speed * BulletBase._simulateSpeed;
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
                }
            }
        }
        return _returnTarget;
    }

    IEnumerator FirstCoroutine()
    {
        float delta = 0;
        while (true)
        {
            delta += Time.deltaTime * BulletBase._simulateSpeed;
            if (delta > 0.2f)
            {
                _firstActionEnd = true;
                yield break;
            }
            else
            {
                _rb2d.linearVelocity = new Vector3(Mathf.Cos(_firstAngle * Mathf.Deg2Rad), Mathf.Sin(_firstAngle * Mathf.Deg2Rad), 0) * _speed * BulletBase._simulateSpeed;
                yield return null;
            }
        }
    }
}
