using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 1.0f;
    public float inputGamma = 0.5f;
    public float dragX = 0.8f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inX = Input.GetAxisRaw("Horizontal");
        float signX = Mathf.Sign(inX);
        float magX = Mathf.Pow(Mathf.Abs(inX), inputGamma);
        float dx = signX * magX * speed;
        Debug.Log(dx);

        Vector2 v = rb.velocity;
        v.x = dragX * v.x + dx;
        rb.velocity = v;
    }
}
