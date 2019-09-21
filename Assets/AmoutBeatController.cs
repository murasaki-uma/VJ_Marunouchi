using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmoutBeatController : MonoBehaviour
{
    // Start is called before the first frame update
    private Material _material;
    public float MaxAmout = 0.5f;
    private float _amount = 0f;
    public float Speed = 0.1f;
    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        
    }

    public void OnBeat()
    {
        _amount = MaxAmout;
    }

    // Update is called once per frame
    void Update()
    {
        _amount += (0f - _amount) * Speed;
        _material.SetFloat("_Amount", _amount);
    }
}
