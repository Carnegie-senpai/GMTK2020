using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_controller : MonoBehaviour
{
    GameObject Player;
    public float leftBound = 0.0f;
    public float rightBound = 0.0f;
    public float lowBound = 0.0f;
    public float topBound = 0.0f;
    private Vector3 center;
    // Start is called before the first frame update
    void Start()
    {
       Player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        center = Player.transform.position;
        if (center.x < rightBound && center.x > leftBound)
        {
            gameObject.transform.position = new Vector3(center.x, 0f, -10);

            if (center.y < topBound && center.y-5.32f > lowBound)
            {
                gameObject.transform.position = new Vector3(center.x, center.y, -10);
            }
            else
            {
                gameObject.transform.position = new Vector3(center.x, 0.1f, -10);

            }
        }
    }
}
