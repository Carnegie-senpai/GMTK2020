using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHandler : MonoBehaviour
{
    public GameObject TextPrefab;
    public GameObject Canvas;
    public List<string> messages;


    private GameObject instance;

    private GameObject textObject;
    private Text text;
    private float time;
    private bool enabled;   // Has collided with the player
    private bool active;    // Whether the text is currently executing a task
    private string texts;   // text to appear on screen
    private int spot;   // Which place in the array of messages it is at
    private int task;   //-1 not assigned
                        // 0 scrolling
                        // 1 deleting
                        // 2 clearing
                   
    private int waitfor; // amount of time for the text to idle
    private int scrollcount;
    private int deletecount;
    private int deleteamount;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Awake()
    {
        task = -1;
        spot = 0;
        active = true;
        scrollcount = 0;
        textObject = Instantiate(TextPrefab, Canvas.transform);
        text = textObject.GetComponent<Text>();
        text.text = "";
        deletecount = 0;
        deleteamount = 0;
        enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            active = false;
            time = Time.time;
            enabled = true;
        }
     /*   texts = "";
        for (int i = 0; i < messages.Count; i++)
        {
            int test = 0;
            int.TryParse(messages[i], out test);
            if (test > 0)
            {
                DeletingText();
            }
            else
            {
                texts += messages[i] + "\n";

            }
        }*/
    }



    void ClearText()
    {
        List<GameObject> targets = new List<GameObject>();
        for (int i = 0; i < Canvas.transform.GetChildCount();i++)
        {
            GameObject g = Canvas.transform.GetChild(i).gameObject;
            targets.Add(g);
        }
        for (int i = 0; i < Canvas.transform.GetChildCount(); i++)
        {
            Destroy(targets[i]);
        }
    }

    private void Update()
    {
        if (enabled)
        {
            if (!active)
            {
                int test = 0;
                int.TryParse(messages[spot], out test);
                if (messages[spot][0] == '@')  //idle
                {
                    task = -1;
                    string temp = messages[spot].Substring(1, messages[spot].Length - 1);
                    int.TryParse(temp, out waitfor);
                    time = Time.time;
                }
                else if (messages[spot][0] == '!') //clear
                {
                    task = 2;

                }
                else if (test > 0)
                {
                    task = 1;
                    deleteamount = test;
                    time = Time.time;
                }
                else
                {

                    task = 0;
                    time = Time.time;
                }
                active = true;
            }
            else
            {
                if (task < 0)  // idle
                {
                    if (Time.time - time > waitfor)
                    {
                        active = false;
                        spot++;
                    }
                }
                else if (task == 0) // scrolling
                {

                    if (scrollcount < messages[spot].Length)
                    {
                        if (Time.time - time > 0.05)
                        {
                            if (messages[spot][scrollcount] == '^')
                            {
                                text.text += "\n";

                            }
                            else
                            {
                                text.text += messages[spot][scrollcount];
                            }
                            scrollcount++;
                            time = Time.time;
                        }
                    }
                    else
                    {
                        active = false;
                        scrollcount = 0;
                        spot++;
                    }
                }
                else if (task == 1)// deleting
                {
                    if (deletecount < deleteamount)
                    {
                        if (Time.time - time > 0.05)
                        {
                            text.text = text.text.Substring(0, text.text.Length - 1);
                            time = Time.time;
                            deletecount++;
                        }
                    }
                    else
                    {
                        active = false;
                        deletecount = 0;
                        spot++;
                    }

                }
                else if (task == 2)// clear
                {
                    ClearText();
                }
            }
        }
    }
}
