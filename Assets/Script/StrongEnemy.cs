using UnityEngine;

public class StrongEnemy : EnemyBase
{
    [Header("PowerUpRate"), Tooltip("上昇量を百分率で設定")]
    [SerializeField]
    float _powerUpRate;

    public override float Attack()
    {
        _combo++;
        return _attackPower * (1 + (_combo - 1) * _powerUpRate);
    }

    public override void Damage()
    {
        _combo = 0;
        _hp -= _player.GetComponent<PlayerShot>().GetAttackPower();
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
