using UnityEngine;
using static UnityEngine.ParticleSystem;

public class HighSpeedEnemy : EnemyBase
{
    [Header("SpeedUpRate"), Tooltip("�㏸�ʂ�S�����Őݒ�")]
    [SerializeField]
    float _speedUpRate;

    public override float Attack()
    {
        _combo++;
        _speed /= 1 + (_combo - 1);//�����l�ɖ߂�
        _speed *= 1 + _combo * _speedUpRate;//�{����������
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
            Instantiate(_particle, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("�p�[�e�B�N�����o�^����Ă��܂���");
        }
        Destroy(gameObject);
    }
}
