using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWiggler : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera _camera;
    private float _startFov;
    public float EndFov;
    public float Speed;
    void Start()
    {
        _camera = GetComponent<Camera>();
        _startFov = _camera.fieldOfView;
//        _endFov = _startFov * 1.3f;

    }

    public void OnEvent()
    {
        _camera.fieldOfView = EndFov;
    }

    // Update is called once per frame
    void Update()
    {
        _camera.fieldOfView += (_startFov - _camera.fieldOfView) * Speed;
    }
}
