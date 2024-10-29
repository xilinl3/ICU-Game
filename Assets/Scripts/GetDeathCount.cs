using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDeathCount : MonoBehaviour
{
    int deathCount = Black.deadNumber;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(deathCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
