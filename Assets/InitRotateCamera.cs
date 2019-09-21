using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InitRotateCamera : MonoBehaviour
{
    private Button _eventButton;
    // Start is called before the first frame update
    void Start()
    {
        _eventButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            ExecuteEvents.Execute
            (
                target      : _eventButton.gameObject,
                eventData   : new PointerEventData( EventSystem.current ),
                functor     : ExecuteEvents.pointerClickHandler
            );
        }
    }
}
