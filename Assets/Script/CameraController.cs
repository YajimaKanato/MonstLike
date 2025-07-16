using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject _player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        if (_player == null)
        {
            Debug.LogWarning("Player‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            transform.position = _player.transform.position + new Vector3(0, 0, -10);
        }
    }
}
