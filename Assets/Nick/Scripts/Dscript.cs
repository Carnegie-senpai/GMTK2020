using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class Dscript : MonoBehaviour
{
    public List<string> message;
    private GameObject Canvas;
    private TextHandler handler;
    // Start is called before the first frame update
    void Awake()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        handler = Canvas.GetComponent<TextHandler>();
    }

    // Update is called once per frame


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            handler.messages = message;
            handler.spot = 0;
            handler.text.text = "";
            handler.active = false;
            handler.time = Time.time;
            handler.enabled = true;
            handler.task = -1;
            handler.scrollcount = 0;
            handler.deletecount = 0;
            Destroy(gameObject);
        }
    }
}
