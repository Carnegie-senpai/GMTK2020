using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject left;
    public GameObject right;
    public GameObject gnome;
    public float direction;

    private Quaternion q;
    // Start is called before the first frame update
    void Awake()
    {
        if (direction  > 0)
        {
            gnome.transform.localScale = new Vector3(-1.0f, 1f, 1f);
        }
        q = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gnome.transform.position.x < left.transform.position.x)
        {
            direction = Mathf.Abs(direction);
            gnome.transform.localScale = new Vector3(-1.0f, 1f, 1f);
        }
        else if (gnome.transform.position.x > right.transform.position.x)
        {
            direction = -Mathf.Abs(direction);
            gnome.transform.localScale = new Vector3(1.0f, 1f, 1f);
        }
        gnome.transform.SetPositionAndRotation(new Vector3(gnome.transform.position.x + direction * Time.deltaTime, gnome.transform.position.y, gnome.transform.position.z), q);
    }

}


