using UnityEngine;

public class HighSpeedEnemy : EnemyBase
{
    [Header("SpeedUpRate"), Tooltip("ã¸—Ê‚ğ•S•ª—¦‚Åİ’è")]
    [SerializeField]
    float _speedUpRate;

    public override float Attack()
    {
        _combo++;
        _speed /= 1 + (_combo - 1);//‰Šú’l‚É–ß‚·
        _speed *= 1 + _combo * _speedUpRate;//”{—¦‚ğ‚©‚¯‚é
        return _attackPower;
    }

    public override void Damage()
    {
        _combo = 0;
        _hp -= _player.GetComponent<PlayerShot>().GetAttackPower();
    }
}
