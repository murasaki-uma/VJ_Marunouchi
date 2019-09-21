using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitSlider : MonoBehaviour
{
    public float InitValue;

    private Slider _slider;
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    
    public void Init()
    {
        _slider.value = InitValue;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
