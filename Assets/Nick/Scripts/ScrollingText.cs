using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public string message;
    private float time;
    private Text text;
    private int count;
    // Update is called once per frame
    void Awake()
    {
        time = Time.time;
        text = gameObject.GetComponent<Text>();
        text.text = "";
        count = 0;
    }
    private void Update()
    {
        if (Time.time - time > .05)
        {
            if (count < message.Length)
            {
                text.text += message[count];
                count++;
                time = Time.time;
                Debug.Log(count);
            }
        }
    }
}
