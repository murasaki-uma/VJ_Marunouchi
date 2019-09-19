using System.Collections;
using System.Collections.Generic;
using SplineMesh;
using UnityEngine;

public class NodeAnimater : MonoBehaviour
{
    private Spline _spline;
    private SplineMeshTiling _splineMeshTiling;
   
    // Start is called before the first frame update
    void Start()
    {
        _spline = GetComponent<Spline>();
        _splineMeshTiling = GetComponent<SplineMeshTiling>();
    }

    // Update is called once per frame
    void Update()
    {
        _spline.nodes[0].Position = new Vector3(Mathf.Sin(Time.time*0.1f) * 10f, 0f,0f);
//        _splineMeshTiling.CreateMeshes();
//        _spline.CurveChanged += _splineMeshTiling.CreateMeshes();
    }
}
