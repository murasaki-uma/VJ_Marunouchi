using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BoneAnimater : MonoBehaviour
{
    public List<GameObject> Bones = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int GetBoneCount()
    {
        return Bones.Count;
    }

    public void SetBoneTransoform(int num, Vector3 position, Vector3 angle)
    {
        if (num < Bones.Count)
        {
            Bones[num].transform.position = position;
            Bones[num].transform.eulerAngles = angle;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
