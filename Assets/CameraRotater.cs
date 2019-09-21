using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraRotater : MonoBehaviour
{
    public float Speed = 0.1f;

    public float Radius = 4;

    public UnityEngine.UI.Slider RadiusSlider;
    public UnityEngine.UI.Slider FovSlider;
    private float _fov;

    public Vector3 OffsetPosition;
    private float _angle = 0;

    private Camera _camera;
    public bool RotateReverse = false;
    public GameObject LookAt;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        _fov = _camera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("."))
        {
            RotateReverse = false;
        }

        if (Input.GetKeyDown(","))
        {
            RotateReverse = true;
        }
        _camera.fieldOfView += (FovSlider.value - _camera.fieldOfView) * 0.1f;
        Radius += (RadiusSlider.value - Radius) * 0.1f;
        _angle += Speed * (RotateReverse ? -2 : 1);
        var x = Mathf.Cos(_angle) * Radius;
        var z = Mathf.Sin(_angle) * Radius;
        var y = 0;
        transform.localPosition = OffsetPosition + new Vector3(x, y, z);
        transform.LookAt(LookAt.transform.position);
    }
}
