using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneMover : MonoBehaviour
{
    public GameObject BonedObject;
    private BoneAnimater _boneAnimater;

    private TransformRecorder _transformRecorder;
    // Start is called before the first frame update
    void Start()
    {
        _transformRecorder = GetComponent<TransformRecorder>();
        var cable = Instantiate(BonedObject, transform);
        _boneAnimater = cable.GetComponent<BoneAnimater>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_transformRecorder.RecordDatas.Count == _transformRecorder.MaxRecordNum)
        {
            var max = _boneAnimater.Bones.Count-1;
            var count = 0;
            foreach (var b in _boneAnimater.Bones)
            {
                var th = (float) count / (float) max;
                var num = Mathf.Lerp(0, _transformRecorder.MaxRecordNum-1, th);
                int i = Mathf.FloorToInt(num);

                var data = _transformRecorder.RecordDatas[i];

//                b.transform.eulerAngles = data.Angle;
                b.transform.position = data.Position;

                count++;

            }
        }
    }
}
