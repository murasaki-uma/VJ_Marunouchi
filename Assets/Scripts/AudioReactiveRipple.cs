using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReactiveRipple : MonoBehaviour
{
    public Material Material;
    
    private float[] _forces = new float[10];
    // Start is called before the first frame update
    void Start()
    {
        if (Material == null)
        {
            Material = GetComponent<MeshRenderer>().material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _forces[0] = Time.time*0.1f % 0.5f;
        
        Material.SetFloatArray("_Forces", _forces);
    }
}
