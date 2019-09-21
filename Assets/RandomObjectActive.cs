using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomObjectActive : MonoBehaviour
{
    public float Duration = 2f;
    public List<GameObject> Objects;

    public int Timing = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void StartMotion()
    {
        LeanTween.cancel(gameObject);
        LeanTween.value(gameObject, 0, 1, Duration).setOnUpdate((v) =>
        {
            if (Mathf.FloorToInt(v) % Timing == 0)
            {
                
            }

            if (Time.frameCount % Timing == 0)
            {
//                Debug.Log("gachagacha");
                foreach (var o in Objects)
                {
                    o.SetActive(Random.Range(0, 2) == 1);
                }
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            StartMotion();
        }
    }
}
