using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WIndowBellowsAnimation : MonoBehaviour
{
    private Transform _top;
//    public int MaxRecordCount = 60 * 4;
    private List<Vector3> _recordPosition = new List<Vector3>();
    private List<Vector3> _recordScale = new List<Vector3>();

//    public int RecordFrameRate = 10;
    public UpdateFrameRate UpdateFrameRate;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform a in transform)
        {
            Debug.Log(a.name);
        }
        _top = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
       

        if (Time.frameCount % UpdateFrameRate.FrameRate == 0)
        {
            Debug.Log(Time.frameCount);
            _recordPosition.Add(_top.localPosition);
            _recordScale.Add(_top.localScale);
        }
        while (_recordPosition.Count > transform.childCount)
        {
            _recordPosition.RemoveAt(0);
        }
        
        while (_recordScale.Count > transform.childCount)
        {
            _recordScale.RemoveAt(0);
        }

       
        var count = 0;
//        var count_each = MaxRecordCount / (transform.childCount - 1);
        var delay = 0;
        if (_recordPosition.Count == transform.childCount)
        {
            foreach (Transform child in transform)
            {
                if (count > 0)
                {
                    child.localPosition = _recordPosition[_recordPosition.Count-1 - count];


                }
                count ++;
            }
        }


        count = 0;
        if (_recordScale.Count == transform.childCount)
        {
            foreach (Transform child in transform)
            {
                if (count > 0)
                {
                    child.localScale = _recordScale[_recordScale.Count-1 - count];


                }
                count ++;
            }
        }
      

    }
}
