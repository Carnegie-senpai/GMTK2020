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
    private Transform trans;
    public GameObject stationary;
    private GameObject s;
    private string messagecopy;
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
        if (count < amount)
        {
            if (Time.time - time > .05)
            {
                text.text = text.text.Substring(0, text.text.Length - 1);
                time = Time.time;
                count++;
            }
            
        }
        else
        {
            trans = gameObject.transform;
            Destroy(gameObject);
            s = Instantiate(stationary, gameObject.transform.parent); 
            messagecopy = text.text;
            text = s.GetComponent<Text>();
            text.text = messagecopy;
        }
    }
}
