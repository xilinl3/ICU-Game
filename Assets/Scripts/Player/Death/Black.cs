using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black : MonoBehaviour
{
    // Start is called before the first frame update
    public static int deadNumber =0;
    void Start()
    {
    }

    void OnEnable()
    {
        deadNumber ++;
    }
}
