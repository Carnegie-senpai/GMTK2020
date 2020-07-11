using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHandler : MonoBehaviour
{
    public GameObject prefab;
    private ScrollingText scrolltext;
    private string texts;
    private GameObject instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Awake()
    {
        texts = "Hello World!\\nThis is a test.";
        instance = Instantiate(prefab,gameObject.transform);
        scrolltext = instance.GetComponent<ScrollingText>();
        scrolltext.message = texts;
        //scrolltext.count = 0;
    }
}
