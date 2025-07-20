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

    public override void Die()
    {
        if (_particle)
        {
            Instantiate(_particle, transform.position, _particle.transform.rotation);
        }
        else
        {
            Debug.LogWarning("�p�[�e�B�N�����o�^����Ă��܂���");
        }
        Destroy(gameObject);
    }
}
