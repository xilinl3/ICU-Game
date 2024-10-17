using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCross : MonoBehaviour
{

    [SerializeField] private string objectColor;


    private void SwitchTo(bool state)
    {
        this.gameObject.GetComponent<Renderer>().enabled = state;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = state;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag(objectColor))
        {
            Debug.Log("Light Enter " + other.gameObject);
            SwitchTo(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(objectColor))
        {
            Debug.Log("Light Exit " + other.gameObject);
            SwitchTo(true);
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
