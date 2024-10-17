using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceManager : MonoBehaviour
{
    private bool playerInside = false;
    private Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<Player>();
            //Debug.Log("Enter");
            player.biteEnable = true;
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Exit");
            player = other.gameObject.GetComponent<Player>();
            player.biteEnable = false;
            playerInside = false;
        }
    }

    private

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (playerInside && player.isBiting)
        {
            Debug.Log("Destroy");

            this.gameObject.GetComponent<Renderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false; // 由于会摧毁第一个Collider，需要将墙壁的碰撞器放在第一个，以防把触发器删了后保留墙体碰撞
        }
    }
}
