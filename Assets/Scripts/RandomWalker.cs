using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalker : MonoBehaviour
{
    public Vector3 StartPos;

    public Vector3 EndPos;

    public float Speed = 0.05f;

    private float _threshold = 0f;

    private Vector3 _baseScale;

    public float NoiseScale = 0.1f;
    public UpdateFrameRate UpdateFrameRate;
    // Start is called before the first frame update
    void Start()
    {
        _baseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.frameCount % UpdateFrameRate.FrameRate != 0) return;
        
        _threshold = (_threshold + Speed) % 1f;

        transform.localPosition = Vector3.Lerp(StartPos, EndPos, _threshold);
        var s = Mathf.PerlinNoise(transform.localPosition.x * NoiseScale, transform.localPosition.y * NoiseScale);
        transform.localScale = _baseScale * (0.5f + s * 0.5f);

    }
}
