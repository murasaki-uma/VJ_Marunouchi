using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class SliderValueToBpm : MonoBehaviour
{
    public Slider Slider;
    public BeatEffect BeatEffect;
    // Start is called before the first frame update
    void Start()
    {
//        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        BeatEffect.BPM = Mathf.FloorToInt(Slider.value);
    }
}
