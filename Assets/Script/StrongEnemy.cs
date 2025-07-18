using UnityEngine;

public class StrongEnemy : EnemyBase
{
    [Header("PowerUpRate"), Tooltip("�㏸�ʂ�S�����Őݒ�")]
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
}
