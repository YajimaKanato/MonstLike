using UnityEngine;
using UnityEngine.EventSystems;

public class DragAction : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("SpeedDown")]
    [SerializeField]
    float _speedDown = 0.2f;

    bool _isDragging = false;

    /// <summary>
    /// マウスドラッグ開始を検知
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            if (!player.GetComponent<PlayerShot>().GetState())
            {
                _isDragging = true;
                PlayerShot.SimulateSpeed = _speedDown;
                Particle.SimulateSpeed = _speedDown;
                EnemyBase.SimulateSpeed = _speedDown;
                BulletBase.SimulateSpeed = _speedDown;
                FriendlyObjectBase.SimulateSpeed = _speedDown;
                GameObject.Find("Main Camera").GetComponent<CameraController>().ZoomIn();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    /// <summary>
    /// マウスドラッグ終了を検知
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isDragging)
        {
            _isDragging = false;
            PlayerShot.SimulateSpeed = 1;
            Particle.SimulateSpeed = 1;
            EnemyBase.SimulateSpeed = 1;
            BulletBase.SimulateSpeed = 1;
            FriendlyObjectBase.SimulateSpeed = 1;
            GameObject.Find("Main Camera").GetComponent<CameraController>().ZoomOut();
        }
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
