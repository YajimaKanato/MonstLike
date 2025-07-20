using UnityEngine;

public class SpreadBullet : FriendlyObjectBase
{
    [Header("SpreadBulletPrameter")]
    [SerializeField, Tooltip("‘Å‚¿o‚·‰ñ”")]
    int _shotNumber = 1;

    [SerializeField, Tooltip("‰½“xŠÔŠu‚Å‘Å‚¿o‚·‚©")]
    float _degree = 10;

    [Header("Bullet")]
    [SerializeField]
    GameObject _bullet;

    protected override void FriendAttack()
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
        }
    }
}
