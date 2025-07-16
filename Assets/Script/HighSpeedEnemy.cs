using UnityEngine;

public class HighSpeedEnemy : EnemyBase
{
    public override float Attack()
    {
        _speed *= 1.1f;
        return _attackPower;
    }
}
