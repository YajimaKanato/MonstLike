using UnityEngine;

public class ToughEnemy : EnemyBase
{
    [Header("ResistRate"), Tooltip("ã¸—Ê‚ğ•S•ª—¦‚Åİ’è")]
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
}
