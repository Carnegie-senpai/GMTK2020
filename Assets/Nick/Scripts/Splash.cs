using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    // Start is called before the first frame update

    private float time;

    void Awake()
    {
        time = Time.time;     
    }

    private void FixedUpdate()
    {
        if (Time.time - time > 1.5)
        {
            Destroy(gameObject);
        }
    }
}
