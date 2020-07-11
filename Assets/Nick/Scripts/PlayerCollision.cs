using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Ow I am dead daddy");
            //player.SetActive(false);
            player.GetComponent<PlayerController>().Respawn();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            player.GetComponent<PlayerController>().RespawnPoint = collision.gameObject.transform.position;
            Debug.Log("Respawn set to " + collision.gameObject.transform.position);
        }
    }
}
