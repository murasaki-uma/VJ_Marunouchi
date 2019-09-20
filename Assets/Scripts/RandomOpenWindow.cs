using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOpenWindow : MonoBehaviour
{
    public float Speed = 0.1f;

    private float _threshold = 0f;
//    public int NextUpdateTiming = 10;
    public UpdateFrameRate UpdateFrameRate;
    public Vector2 Resolution = new Vector2(1000f,1000f);
    // Start is called before the first frame update
    private LineRenderer _lineRenderer;
    public GameObject BaseWindow;
    private List<Vector3> _positions = new List<Vector3>();
    private float _scale = 0f;
    private Vector3 _centerPositon = Vector3.zero;
    private Vector3 _nextPositon = Vector3.zero;
    private Vector3 _startPosition = Vector3.zero;
    private Vector3 _baseScale;
    public Material Material;
    public Camera OffscreenCamera;
    void Start()
    {
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.positionCount = 4;
        _lineRenderer.startWidth = 4;
        _lineRenderer.endWidth = 4;
        _lineRenderer.loop = true;
        _lineRenderer.sharedMaterial = Material;

        _baseScale= BaseWindow.transform.localScale;
        _positions.Add(new Vector3(-_baseScale.x/2f,_baseScale.y/2f, 0f));
        _positions.Add(new Vector3(-_baseScale.x/2f,-_baseScale.y/2f, 0f));
        _positions.Add(new Vector3(_baseScale.x/2f,-_baseScale.y/2f, 0f));
        _positions.Add(new Vector3(_baseScale.x/2f,_baseScale.y/2f, 0f));
        
        
        BaseWindow.SetActive(false);
        OffscreenCamera.gameObject.SetActive(false);
        _nextPositon = new Vector3(Random.Range(-Resolution.x/2f,Resolution.x/2f), Random.Range(Resolution.y/2f,-Resolution.y/2), 0f);
        

    }

    // Update is called once per frame
    void Update()
    {
//        if (Time.frameCount % NextUpdateTiming == 0)
//        {
//            _centerPositon = new Vector3(Random.Range(-Resolution.x/2f,Resolution.x/2f), Random.Range(Resolution.y/2f,-Resolution.y/2), 0f);
//
//        }

        if (Time.frameCount % UpdateFrameRate.FrameRate == 0)
        {
            _threshold += Speed;
            if (_threshold > 1.6f)
            {
                _startPosition = _centerPositon;
                _nextPositon = new Vector3(Random.Range(-Resolution.x/2f,Resolution.x/2f), Random.Range(Resolution.y/2f,-Resolution.y/2), 0f);
                _threshold = 0;

            }

           
            
            _centerPositon = Vector3.Lerp(_startPosition, _nextPositon, Mathf.Min(_threshold,1f));
                
            _positions[0] = (new Vector3(-_baseScale.x/2f,_baseScale.y/2f, 0f) + _centerPositon);
            _positions[1] = (new Vector3(-_baseScale.x/2f,-_baseScale.y/2f, 0f) + _centerPositon);
            _positions[2] = (new Vector3(_baseScale.x/2f,-_baseScale.y/2f, 0f) + _centerPositon);
            _positions[3] = (new Vector3(_baseScale.x/2f,_baseScale.y/2f, 0f) + _centerPositon);
                
            _lineRenderer.SetPositions(_positions.ToArray());
            OffscreenCamera.transform.localPosition = new Vector3(_centerPositon.x * 0.01f, _centerPositon.y * 0.01f+10f,-20);
            
            if (_threshold >= 1.0)
            {
                OffscreenCamera.Render();
                _lineRenderer.positionCount = 0;
                BaseWindow.SetActive(true);
                BaseWindow.transform.localPosition = _centerPositon;
                
            }
            else
            {
//                OffscreenCamera.gameObject.SetActive(false);
                _lineRenderer.positionCount = 4;
                BaseWindow.SetActive(false);
            }
            
            
        }
    }
}
