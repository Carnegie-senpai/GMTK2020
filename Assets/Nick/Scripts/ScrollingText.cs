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
    private Transform trans;
    public GameObject stationary;
    private GameObject s;
    private string messagecopy;
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
        if (count < message.Length)
        {
            if (Time.time - time > .05)   
            {
                text.text += message[count];
                count++;
                time = Time.time;
            }
        }
        else
        {
            trans = gameObject.transform;
            Destroy(gameObject);
            s = Instantiate(stationary, gameObject.transform.parent);
            text = s.GetComponent<Text>();
            text.text = message;
        }
    }
}
