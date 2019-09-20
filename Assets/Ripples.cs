using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripples : MonoBehaviour
{
    public int Size = 4;
    private List<Circle> _circles = new List<Circle>();
    private List<float> _radius = new List<float>();

    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Size; i++)
        {
            var circle = new GameObject("circle").AddComponent<Circle>();
            circle.Init(material);
            _radius.Add(0);
            _circles.Add(circle);
            
            circle.transform.SetParent(transform);
            circle.transform.localPosition = Vector3.zero;
        }
    }

    public void StartMotion(float duration)
    {
        for (int i = 0; i < Size; i++)
        {
            var radius = Random.Range(100f, 200f);
            var position = new Vector3(Random.Range(-500,500f),Random.Range(-500,500f),0);
            var circle_duration = duration * Random.Range(1f, 0.8f);
            var circle_delay = duration - circle_duration;
            _circles[i].transform.localPosition = position;
            var circle = _circles[i];
            LeanTween.value(circle.gameObject, 0f, radius, circle_duration).setOnUpdate((v) =>
            {
                circle.Radius = v;
                circle.UpdateCircle();
            }).setOnComplete(() => { circle.Radius = 0; circle.UpdateCircle();}).setDelay(circle_delay);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            StartMotion(1f);
        }
    }
}
