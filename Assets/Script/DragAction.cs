using UnityEngine;
using UnityEngine.EventSystems;

public class DragAction : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    [Header("SpeedDown")]
    [SerializeField]
    float _speedDown = 0.2f;

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
                //すべてのゲームオブジェクトを取得
                var go = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
                foreach (var sim in go)
                {
                    //ISimulateインターフェースを取得できた場合に行う
                    sim.GetComponent<ISimulate>()?.SimulateChange(_speedDown);
                }

                GameObject.Find("Main Camera").GetComponent<Camera>().fieldOfView = 60;
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
        var go = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (var sim in go)
        {
            sim.GetComponent<ISimulate>()?.SimulateChange(1);
        }

        GameObject.Find("Main Camera").GetComponent<Camera>().fieldOfView = 80;
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
