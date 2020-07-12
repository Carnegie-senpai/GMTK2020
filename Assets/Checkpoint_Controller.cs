using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public int Moveset = 0;
    public GameObject player;
    public bool isEnd = false;
    public GameObject EndPanel;
    private float time;
    private bool Triggered = false;
    //public GameObject endButton;
    int i = 0;
    void Start()
    {
       player = GameObject.Find("Player");
        EndPanel = GameObject.Find("EndPanel");
      //  endButton = GameObject.Find("endButton");
    }

    private void Update()
    {
        if (isEnd && Triggered)
        {
            if(i <= 9)
            {
                if (Time.time - time > 0.5)
                {
                    time = Time.time;
                    if (!EndPanel.transform.GetChild(i).gameObject.activeSelf)
                    {
                        EndPanel.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    i++;
                }
            }
            if (i == 10 && Time.time - time > 5.0) {
                EndPanel.transform.GetChild(i).gameObject.SetActive(true);
            
            }

        }
    }

    private void Endgame() { 
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerController>().RespawnPoint = collision.gameObject.transform.position;
            Debug.Log("Respawn set to " + collision.gameObject.transform.position);
            if (player.GetComponent<PlayerController>().currentInputSet < Moveset)
                player.GetComponent<PlayerController>().currentInputSet = Moveset;
            if (isEnd) {
                Triggered = true;
            }
            
        }
    }
}
