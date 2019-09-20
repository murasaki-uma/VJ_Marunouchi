using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisable : MonoBehaviour
{
    public List<GameObject> Objes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void AllDisable()
    {
        foreach (var VARIABLE in Objes)
        {
            VARIABLE.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
