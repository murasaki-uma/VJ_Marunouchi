using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatEffect : MonoBehaviour
{
    private float BPM = 120;

    private int timing = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timing = Mathf.FloorToInt((60 * 60) / 120f);
        if (Time.frameCount % timing == 0)
        {
            Debug.Log("Beat!");
        }

    }
}
