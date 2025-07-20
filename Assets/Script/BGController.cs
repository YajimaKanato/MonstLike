using UnityEngine;

public class BGController : MonoBehaviour
{
    [Header("MovingObject"), Tooltip("���삷��I�u�W�F�N�g�i�v���C���[�Ȃǁj��ݒ肵�Ă�������")]
    [SerializeField]
    GameObject _movingObject;

    [Header("SpriteRenderer"), Tooltip("�w�i�̃X�v���C�g��ݒ肵�Ă�������")]
    [SerializeField]
    SpriteRenderer _spriteRenderer;

    [Header("BackGround"), Tooltip("�������w�i�I�u�W�F�N�g")]
    [SerializeField]
    GameObject _backGround;

    [Header("MoveRate"), Tooltip("���삷��I�u�W�F�N�g�ɑ΂��Ăǂꂭ�炢�̑��x�œ�������")]
    [SerializeField]
    float _moveRate;

    Rigidbody2D _movingObjectRb2d;
    Rigidbody2D _rb2d;

    Vector3 _basePos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!_movingObject)
        {
            Debug.LogWarning("�I�u�W�F�N�g���ݒ肳��Ă��܂���");
        }
        else
        {
            _movingObjectRb2d = _movingObject.GetComponent<Rigidbody2D>();
            if (_movingObjectRb2d)
            {
                //Rigidbody�ŊǗ�
                _backGround.AddComponent<Rigidbody2D>();
                _rb2d = _backGround.GetComponent<Rigidbody2D>();
                _rb2d.gravityScale = 0;
            }
            else
            {
                Debug.Log("�������I�u�W�F�N�g�ɂ�Rigidbody������܂���");
                _basePos = _backGround.transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //���삷��I�u�W�F�N�g��Rigidbody�����邩�ǂ����œ�������ς���
        if (_movingObjectRb2d)
        {
            _rb2d.linearVelocity = _movingObjectRb2d.linearVelocity * _moveRate;
        }
        else
        {
            _backGround.transform.position += (_movingObject.transform.position - _basePos) * _moveRate;
        }

        if (Mathf.Abs(_movingObject.transform.position.x - _backGround.transform.position.x) >= _spriteRenderer.bounds.size.x ||
            Mathf.Abs(_movingObject.transform.position.y - _backGround.transform.position.y) >= _spriteRenderer.bounds.size.y)
        {//�������I�u�W�F�N�g��Sprite�̃T�C�Y�̕���������������W����
            _backGround.transform.position = _movingObject.transform.position;
        }
    }
}
