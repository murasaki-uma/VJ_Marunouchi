using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RecordData
{
    public Vector3 Position;
    public Vector3 Angle;
}
public class TransformRecorder : MonoBehaviour
{
    
    private LineRenderer _lineRenderer;

    private List<Vector3> _positions = new List<Vector3>();
//    private List<Vector3> _angles = new List<Vector3>();
    
    private List<RecordData> _recordDatas = new List<RecordData>();

    public int MaxRecordNum = 200;

    public int RecordFrameRate = 1;
    // Start is called before the first frame update
    void Start()
    {
//        _lineRenderer = gameObject.AddComponent<LineRenderer>();
//
//        _lineRenderer.startWidth = 1;
//        _lineRenderer.endWidth = 1;
        
    }

    public List<RecordData> RecordDatas
    {
        get => _recordDatas;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % RecordFrameRate == 0)
        {
            var data = new RecordData();
            data.Position = transform.position;
            data.Angle = transform.eulerAngles;
            _recordDatas.Add(data);
            _positions.Add(transform.position);
        }

        while (_recordDatas.Count > MaxRecordNum)
        {
            _recordDatas.RemoveAt(0);
            _positions.RemoveAt(0);
        }


        if (_recordDatas.Count == MaxRecordNum)
        {
//            _lineRenderer.numPositions = _positions.Count;
//            _lineRenderer.SetPositions(_positions.ToArray());
        }

//            _lineRenderer.positionCount = _recordDatas.Count;
//            var positions = new Vector3[MaxRecordNum];
//            var count = 0;
//            foreach (var v in _recordDatas)
//            {
//                positions[count] = _recordDatas[count].Position;
//                count++;
//            }
//            _lineRenderer.SetPositions(positions);
//            
//        }
    }
}
