using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle:MonoBehaviour
{
        
    private const int CircleSegmentCount = 64;
    private const int CircleVertexCount = CircleSegmentCount + 2;
    private const int CircleIndexCount = CircleSegmentCount * 3;

    private Mesh _mesh;

    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    public float Radius = 10f;
    private LineRenderer _lineRenderer;
//    public int Segment = 30;
    public void Init(Material material)

    {

        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.positionCount = CircleVertexCount;
        _lineRenderer.loop = true;

        _lineRenderer.startWidth = 5;
        _lineRenderer.endWidth = 5;
        _lineRenderer.material = material;
//        _mesh = new Mesh();
//        _meshFilter = gameObject.AddComponent<MeshFilter>();
//        _meshRenderer = gameObject.AddComponent<MeshRenderer> ();
//        _meshRenderer.material = material;
//        material.SetColor("_Color", Color.white);
//        _meshRenderer.material = material;


    }

    public void UpdateCircle()
    {
//            _mesh.vertices.Clone()
//        _mesh.Clear();
        var vertices = new List<Vector3>(CircleVertexCount);
        var indices = new int[CircleIndexCount];
        var segmentWidth = Mathf.PI * 2f / (CircleSegmentCount-1);
        var angle = 0f;
//        vertices.Add(Vector3.zero);
        for (int i = 0; i < CircleVertexCount; i++)
        {
            vertices.Add(new Vector3(Mathf.Cos(angle)*Radius, Mathf.Sin(angle)*Radius, 0f)+transform.position);
            angle -= segmentWidth;
            if (i > 1)
            {
                var j = (i - 2) * 3;
                indices[j + 0] = 0;
                indices[j + 1] = i - 1;
                indices[j + 2] = i;
            }
        }
//        _mesh.SetVertices(vertices);
//        _mesh.SetIndices(indices, MeshTopology.Triangles, 0);
//        _mesh.RecalculateBounds();

        _lineRenderer.SetPositions(vertices.ToArray());

//        _meshFilter.mesh = _mesh;

    }

    private void Update()
    {
//        UpdateCircle();
    }
}