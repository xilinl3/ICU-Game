using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float chess = GameObject.FindWithTag("Player").GetComponent<player_behaviors>().collectedCheese;
        
    }
}
