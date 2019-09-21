using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class BeatEffect : MonoBehaviour
{
    public int BPM = 120;

    private int timing = 0;

    public Button EventButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timing = Mathf.FloorToInt((60 * 60) / (float)BPM);
        if (Time.frameCount % timing == 0 && BPM != 0)
        {
            ExecuteEvents.Execute
            (
                target      : EventButton.gameObject,
                eventData   : new PointerEventData( EventSystem.current ),
                functor     : ExecuteEvents.pointerClickHandler
            );
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExecuteEvents.Execute
            (
                target      : EventButton.gameObject,
                eventData   : new PointerEventData( EventSystem.current ),
                functor     : ExecuteEvents.pointerClickHandler
            );
        }
        
    }
}
