using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] float _zoomInParam;
    [SerializeField] float _zoomOutParam;
    [SerializeField] float _zoomSpeed;

    GameObject _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        if (_player == null)
        {
            Debug.LogWarning("Player��������܂���");
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

    /// <summary>
    /// �Y�[���C������֐�
    /// </summary>
    public void ZoomIn()
    {
        Debug.Log("ZoomIn");
        StartCoroutine(ZoomCoroutine(_zoomOutParam, _zoomInParam));
    }

    /// <summary>
    /// �Y�[���A�E�g����֐�
    /// </summary>
    public void ZoomOut()
    {
        Debug.Log("ZoomOut");
        StartCoroutine(ZoomCoroutine(_zoomInParam, _zoomOutParam));
    }

    /// <summary>
    /// �Y�[���C���A�E�g�����炩�ɍs���֐�
    /// </summary>
    /// <param name="startParam"> �n�߂̃Y�[���T�C�Y</param>
    /// <param name="endParam"> �I���̃Y�[���T�C�Y</param>
    /// <returns></returns>
    IEnumerator ZoomCoroutine(float startParam, float endParam)
    {
        float zoomParam = startParam;
        while (true)
        {
            if (Mathf.Abs(startParam - zoomParam) > Mathf.Abs(startParam - endParam))
            {
                yield break;
            }
            else
            {
                zoomParam += (endParam - startParam) * Time.deltaTime / _zoomSpeed;
                GetComponent<Camera>().fieldOfView = zoomParam;
                yield return null;
            }
        }
    }
}
