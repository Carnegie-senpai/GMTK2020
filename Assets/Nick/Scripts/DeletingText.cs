using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeletingText : MonoBehaviour
{
    public string message;
    private float time;
    private Text text;
    private int count;
    public int amount;
    // Start is called before the first frame update
    void Awake()
    {
        time = Time.time;
        text = gameObject.GetComponent<Text>();
        text.text = message;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - time > .05)
        {
            if (count < amount)
            {
                text.text 
            }
        }
    }
}
