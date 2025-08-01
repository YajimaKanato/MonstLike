using UnityEngine;

public class ToughEnemy : EnemyBase
{
    [Header("ResistRate"), Tooltip("上昇量を百分率で設定")]
    [SerializeField]
    float _resistRate;

    public override float Attack()
    {
        _combo++;
        return _attackPower;
    }

    public override void Damage()
    {
        _hp -= _player.GetComponent<PlayerShot>().GetAttackPower() * (1 - (_combo * _resistRate));
        _combo = 0;
    }

    public override void Die()
    {
        if (_particle)
        {
            Instantiate(_particle, transform.position, _particle.transform.rotation);
        }
        else
        {
            Debug.LogWarning("パーティクルが登録されていません");
        }
        Destroy(gameObject);
    }
}
