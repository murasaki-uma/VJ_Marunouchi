using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotater : MonoBehaviour
{
    public float Speed = 0.1f;

    public float Radius = 4;

    public Vector3 OffsetPosition;
    private float _angle = 0;

    private Camera _camera;

    public GameObject LookAt;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        _angle += Speed;
        var x = Mathf.Cos(_angle) * Radius;
        var z = Mathf.Sin(_angle) * Radius;
        var y = 0;
        transform.localPosition = OffsetPosition + new Vector3(x, y, z);
        transform.LookAt(LookAt.transform.position);
    }
}
