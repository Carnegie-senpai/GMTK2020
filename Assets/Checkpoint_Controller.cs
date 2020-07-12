using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public int Moveset = 0;
    public GameObject player;
    void Start()
    {
       player = GameObject.Find("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerController>().RespawnPoint = collision.gameObject.transform.position;
            Debug.Log("Respawn set to " + collision.gameObject.transform.position);
            if (player.GetComponent<PlayerController>().currentInputSet < Moveset)
                player.GetComponent<PlayerController>().currentInputSet = Moveset;
        }
    }
}
