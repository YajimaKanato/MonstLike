using System.Collections;
using UnityEngine;

public class HomingBullet : FriendlyObjectBase
{
    [Header("Number"), Tooltip("‰½”­Œ‚‚¿‚¾‚·‚©")]
    [SerializeField]
    int _num = 5;

    protected override void FriendAttack()
    {
        StartCoroutine(ShotCoroutine());
    }

    IEnumerator ShotCoroutine()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float rad;
        for (int i = 0; i < _num; i++)
        {
            rad = Random.Range(0, 2 * Mathf.PI);
            Instantiate(_bullet,
                transform.position + new Vector3(spriteRenderer.bounds.size.x * Mathf.Cos(rad), spriteRenderer.bounds.size.y * Mathf.Sin(rad), 0),
                transform.rotation * Quaternion.AngleAxis(rad * Mathf.Rad2Deg, Vector3.forward));
            yield return new WaitForSeconds(0.2f);
        }
        yield break;
    }
}
