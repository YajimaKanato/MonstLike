using UnityEngine;
using static UnityEngine.ParticleSystem;

public class HighSpeedEnemy : EnemyBase
{
    [Header("SpeedUpRate"), Tooltip("上昇量を百分率で設定")]
    [SerializeField]
    float _speedUpRate;

    public override float Attack()
    {
        _combo++;
        _speed /= 1 + (_combo - 1);//初期値に戻す
        _speed *= 1 + _combo * _speedUpRate;//倍率をかける
        return _attackPower;
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
