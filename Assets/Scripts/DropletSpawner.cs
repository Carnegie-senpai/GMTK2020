using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject droplet;
    private float time;
    public int droplet_number;
    private int droplet_count;
    public float delay;
    private float spawn_delay;
    private float spawn_time;

    private void Awake()
    {
        time = Time.time;
        spawn_time = Time.time;
        spawn_delay = 0.15f;
        droplet_count = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - time  > delay)
        {
            if (Time.time-spawn_time > spawn_delay)
            {
                if (droplet_count < droplet_number)
                {
                    Instantiate(droplet, transform.position, Quaternion.identity);
                    spawn_time = Time.time;
                    droplet_count++;
                }
                else
                {
                    spawn_time = Time.time;
                    time = Time.time;
                    droplet_count = 0;
                }
            }
        }
    }
}
