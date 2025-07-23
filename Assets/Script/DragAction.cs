using UnityEngine;
using UnityEngine.EventSystems;

public class DragAction : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    [Header("SpeedDown")]
    [SerializeField]
    float _speedDown = 0.2f;

    /// <summary>
    /// �}�E�X�h���b�O�J�n�����m
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            if (!player.GetComponent<PlayerShot>().GetState())
            {
                FriendlyObjectBase.SimulateSpeed = _speedDown;
                EnemyBase.SimulateSpeed = _speedDown;
                BulletBase.SimulateSpeed = _speedDown;
                Particle.SimulateSpeed = _speedDown;
                PlayerShot.SimulateSpeed = _speedDown;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    /// <summary>
    /// �}�E�X�h���b�O�I�������m
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnEndDrag(PointerEventData eventData)
    {
        FriendlyObjectBase.SimulateSpeed = 1;
        EnemyBase.SimulateSpeed = 1;
        BulletBase.SimulateSpeed = 1;
        Particle.SimulateSpeed = 1;
        PlayerShot.SimulateSpeed = 1;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
