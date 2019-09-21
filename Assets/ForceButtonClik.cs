using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ForceButtonClik : MonoBehaviour
{
    public string key = "1";

    private Button _button;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            ExecuteEvents.Execute
            (
                target      : _button.gameObject,
                eventData   : new PointerEventData( EventSystem.current ),
                functor     : ExecuteEvents.pointerClickHandler
            );
        }
    }
}
