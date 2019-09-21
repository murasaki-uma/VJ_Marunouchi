using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGoActive : MonoBehaviour
{
    public string key = "d";

    public GameObject go;

    private bool _active = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            _active = !_active;
            if (_active == false)
            {
                go.transform.localScale = Vector3.zero;
            }
            else
            {
                go.transform.localScale = Vector3.one;
            }
        }
    }
}
