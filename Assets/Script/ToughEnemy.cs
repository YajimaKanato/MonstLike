using UnityEngine;

public class ToughEnemy : EnemyBase
{
    [Header("ResistRate"), Tooltip("�㏸�ʂ�S�����Őݒ�")]
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
            Debug.LogWarning("�p�[�e�B�N�����o�^����Ă��܂���");
        }
        Destroy(gameObject);
    }
}
