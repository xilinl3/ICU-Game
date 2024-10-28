using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class black : MonoBehaviour
{
    // Start is called before the first frame update
    public int deadNumber =0;
    void Start()
    {
    }

    void OnEnable()
    {
        deadNumber ++;
    }
}
