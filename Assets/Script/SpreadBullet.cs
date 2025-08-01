using System.Collections;
using UnityEngine;

public class SpreadBullet : FriendlyObjectBase
{
    [Header("SpreadBulletPrameter")]
    [SerializeField, Tooltip("打ち出す回数")]
    int _shotNumber = 1;

    [SerializeField, Tooltip("何度間隔で打ち出すか")]
    float _degree = 10;

    protected override void FriendAttack()
    {
        StartCoroutine(ShotCoroutine());
    }

    /// <summary>
    /// 拡散に時間差を設ける関数
    /// </summary>
    /// <returns></returns>
    IEnumerator ShotCoroutine()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        for (int i = 0; i < _shotNumber; i++)
        {
            for (int j = 0; j < 360 / _degree; j++)
            {
                Instantiate(_bullet, transform.position +
                    new Vector3(spriteRenderer.bounds.size.x * Mathf.Cos(_degree * j * Mathf.Deg2Rad), spriteRenderer.bounds.size.y * Mathf.Sin(_degree * j * Mathf.Deg2Rad), 0),
                    transform.rotation * Quaternion.AngleAxis(_degree * j, Vector3.forward));
            }
            yield return new WaitForSeconds(0.2f);
        }
        yield break;
    }
}
