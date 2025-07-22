using UnityEngine;
using UnityEngine.EventSystems;

public class DragAction : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    [Header("SpeedDown")]
    [SerializeField]
    float _speedDown = 0.2f;

    GameObject[] _friendlyObjectBase, _EnemyBase, _bulletBase;

    /// <summary>
    /// マウスドラッグ開始を検知
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnBeginDrag(PointerEventData eventData)
    {
        /*_friendlyObjectBase = GameObject.FindGameObjectsWithTag("FriendlyObject");
        _EnemyBase = GameObject.FindGameObjectsWithTag("Enemy");
        _bulletBase = GameObject.FindGameObjectsWithTag("FriendlyBullet");

        if(_friendlyObjectBase.Length>0)
        {
            foreach (var item in _friendlyObjectBase)
            {
                item.GetComponent<FriendlyObjectBase>().SpeedDown(_speedDown);
            }
        }

        if (_EnemyBase.Length > 0)
        {
            foreach (var item in _EnemyBase)
            {
                item.GetComponent<EnemyBase>().SpeedDown(_speedDown);
            }
        }

        if(_bulletBase.Length > 0)
        {
            foreach (var item in _bulletBase)
            {
                item.GetComponent<BulletBase>().SpeedDown(_speedDown);
            }
        }

        _friendlyObjectBase = null;
        _EnemyBase = null;
        _bulletBase = null;*/

        FriendlyObjectBase._simulateSpeed = _speedDown;
        EnemyBase._simulateSpeed = _speedDown;
        BulletBase._simulateSpeed = _speedDown;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    /// <summary>
    /// マウスドラッグ終了を検知
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnEndDrag(PointerEventData eventData)
    {
        /*_friendlyObjectBase = GameObject.FindGameObjectsWithTag("FriendlyObject");
        _EnemyBase = GameObject.FindGameObjectsWithTag("Enemy");
        _bulletBase = GameObject.FindGameObjectsWithTag("FriendlyBullet");
        if(_friendlyObjectBase.Length > 0)
        {
            foreach (var item in _friendlyObjectBase)
            {
                item.GetComponent<FriendlyObjectBase>().SpeedUp(_speedDown);
            }
        }

        if(_EnemyBase.Length > 0)
        {
            foreach (var item in _EnemyBase)
            {
                item.GetComponent<EnemyBase>().SpeedUp(_speedDown);
            }
        }

        if( _bulletBase.Length > 0)
        {
            foreach (var item in _bulletBase)
            {
                item.GetComponent<BulletBase>().SpeedUp(_speedDown);
            }
        }

        _friendlyObjectBase = null;
        _EnemyBase = null;
        _bulletBase = null;*/
        FriendlyObjectBase._simulateSpeed = 1;
        EnemyBase._simulateSpeed = 1;
        BulletBase._simulateSpeed = 1;
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
