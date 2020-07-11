using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause_Controller : MonoBehaviour
{
    public GameObject Pause_menu;
    public GameObject player;
    public GameObject pauseButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.activeSelf)
        {
            if (Input.GetKeyDown("escape"))
            {
                player.GetComponent<PlayerController>().enabled = false;
                Pause_menu.SetActive(true);
                pauseButton.SetActive(true);
            }
        }
        else {
            Pause_menu.SetActive(true);
        }
        
    }
    public void Resume() {
        pauseButton.SetActive(false);
        Pause_menu.SetActive(false);
        player.GetComponent<PlayerController>().enabled = true;
    }
    public void retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
    public void RTM() {
        SceneManager.LoadScene(0);    
    }
}
