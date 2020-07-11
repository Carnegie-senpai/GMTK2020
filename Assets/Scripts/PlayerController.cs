using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState {
        IDLE,
        WALKING,
        JUMPING,
        FREEFALL,
        NUM_STATES
    }

    public float speed = 1.0f;
    public float inputGamma = 0.5f;
    public float dragX = 0.8f;
    public float jumpPower = 10.0f;

    public PlayerState state = PlayerState.IDLE;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update() {
        switch (state) {
            case PlayerState.IDLE:
                if (rb.velocity.y > jumpPower / 2) {
                    state = PlayerState.JUMPING;
                } else if (rb.velocity.y < 0) {
                    state = PlayerState.FREEFALL;
                } else if (rb.velocity.x != 0) {
                    state = PlayerState.WALKING;
                }
                break;
            case PlayerState.WALKING:
                if (rb.velocity.y > jumpPower / 2) {
                    state = PlayerState.JUMPING;
                } else if (rb.velocity.y < 0) {
                    state = PlayerState.FREEFALL;
                } else if (rb.velocity.x == 0) {
                    state = PlayerState.IDLE;
                }
                break;
            case PlayerState.JUMPING:
                if (rb.velocity.y < 0) {
                    state = PlayerState.FREEFALL;
                }
                break;
            case PlayerState.FREEFALL:
                if (rb.velocity.y < 0) {
                    state = PlayerState.FREEFALL;
                } else if (rb.velocity.x != 0) {
                    state = PlayerState.WALKING;
                } else if (rb.velocity.x == 0) {
                    state = PlayerState.IDLE;
                }
                break;
        }
    }

    public void Land() {
        if (state == PlayerState.JUMPING || state == PlayerState.FREEFALL) {
            if (rb.velocity.x != 0) {
                state = PlayerState.WALKING;
            } else {
                state = PlayerState.IDLE;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inX = Input.GetAxisRaw("Horizontal");
        //float signX = Mathf.Sign(inX);
        //float magX = Mathf.Pow(Mathf.Abs(inX), inputGamma);
        //float dx = signX * magX * speed;
        float dx = speed * inX;

        Vector2 v = rb.velocity;
        float inY = Input.GetAxisRaw("Vertical");
        if (inY > 0 && (state == PlayerState.IDLE || state == PlayerState.WALKING)) {
            v = rb.velocity;
            v.y = jumpPower;
            rb.velocity = v;
        }

        v = rb.velocity;
        v.x = dragX * v.x + dx;
        rb.velocity = v;
    }
}
