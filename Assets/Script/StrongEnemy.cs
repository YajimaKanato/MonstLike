using UnityEngine;

public class StrongEnemy : EnemyBase
{
    public override float Attack()
    {
        _attackPower++;
        return _attackPower;
    }
}
