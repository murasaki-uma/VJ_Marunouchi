using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectDisable : MonoBehaviour
{
    public List<GameObject> Objes;

    public List<Toggle> Toggles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AllDisable()
    {
        foreach (var VARIABLE in Objes)
        {
            VARIABLE.SetActive(false);
        }

        foreach (var VARIABLE in Toggles)
        {
            VARIABLE.isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
