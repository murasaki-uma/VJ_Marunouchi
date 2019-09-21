using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSlierValue : MonoBehaviour
{
    public List<Slider> Sliders;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void SetRandomValues()
    {
        foreach (var s in Sliders)
        {
            s.value = Random.Range(s.minValue, s.maxValue);
        }
        
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SetRandomValues();
        }
    }
}
