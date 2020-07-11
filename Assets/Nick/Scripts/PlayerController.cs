using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    public enum PlayerInput {
        NONE,
        LEFT,
        RIGHT,
        UP,
        MOUSE0
    }

    [System.Serializable]
    public class InputSet {
        public PlayerInput left, right, jump, shoot;
        public InputSet(PlayerInput left, PlayerInput right, PlayerInput jump, PlayerInput shoot) {
            this.left = left;
            this.right = right;
            this.jump = jump;
            this.shoot = shoot;
        }
    }

    public Vector3 RespawnPoint = new Vector3(0, 0, 0);
    public float speed = 1.0f;
    public float inputGamma = 0.5f;
    public float dragX = 0.8f;
    public float jumpPower = 10.0f;
    public float shootTime = 1.0f;
    public float shootCooldown = 1.0f;
    public float recoilStrength = 5.0f;
    public Vector2 recoilOffset = new Vector3(0, 1);
    public int currentInputSet = 0;
    public InputSet[] restrictions = {
        new InputSet(PlayerInput.LEFT, PlayerInput.RIGHT, PlayerInput.NONE, PlayerInput.NONE),
    };
    private bool right = true;
    private Vector3 leftScale, rightScale;

    private float shootTimer = 0;
    private float shootCooldownTimer = 0;

    public PlayerState state = PlayerState.IDLE;

    public GameObject sprite;
    
    private Animator anim;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        anim = sprite.GetComponent<Animator>();
        rightScale = sprite.transform.localScale;
        leftScale = new Vector3(-rightScale.x, rightScale.y, rightScale.z);
    }

    private bool checkInput(PlayerInput input) {
        Debug.Log(input);
        switch (input) {
            case PlayerInput.LEFT:
                return Input.GetAxisRaw("Horizontal") < 0;
            case PlayerInput.RIGHT:
                return Input.GetAxisRaw("Horizontal") > 0;
            case PlayerInput.UP:
                return Input.GetAxisRaw("Vertical") > 0;
            case PlayerInput.MOUSE0:
                return Input.GetMouseButton(0);
        }
        return false;
    }

    void Update() {
        // Shooting code (can only handle mouse click input in update
        if (checkInput(restrictions[currentInputSet].shoot) && shootCooldownTimer + shootCooldown < Time.time) {
            Debug.Log("shoot");
            shootTimer = shootCooldownTimer = Time.time;
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 toMouse = (mousePoint - transform.position).normalized;
            toMouse.z = 0;
            rb.velocity -= recoilStrength * (new Vector2(toMouse.x, toMouse.y)) - recoilOffset;
            if (toMouse.x > 0) {
                sprite.transform.localScale = rightScale;
                sprite.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg);
            } else {
                sprite.transform.localScale = leftScale;
                sprite.transform.rotation = Quaternion.Euler(0, 0, 180 + Mathf.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg);
            }
            anim.SetTrigger("shoot");
        }
        // Shot resolution
        if (shootTimer + shootTime < Time.time) {
            sprite.transform.rotation = Quaternion.identity;
            switch (state) {
                case PlayerState.IDLE:
                    if (right) {
                        anim.SetTrigger("idle_R");
                    } else {
                        anim.SetTrigger("idle_L");
                    }
                    break;
                case PlayerState.WALKING:
                    anim.SetTrigger("walk");
                    break;
                case PlayerState.JUMPING:
                    anim.SetTrigger("jump");
                    break;
            }
        }


        // State transition code
        PlayerState nextState = state;
        switch (state) {
            case PlayerState.IDLE:
                if (rb.velocity.y > jumpPower / 2) {
                    nextState = PlayerState.JUMPING;
                } else if (rb.velocity.y < 0) {
                    nextState = PlayerState.FREEFALL;
                } else if (rb.velocity.x != 0) {
                    nextState = PlayerState.WALKING;
                }
                if (shootTimer + shootTime < Time.time) {
                    sprite.transform.localScale = rightScale;
                }
                break;
            case PlayerState.WALKING:
                if (rb.velocity.y > jumpPower / 2) {
                    nextState = PlayerState.JUMPING;
                } else if (rb.velocity.y < 0) {
                    nextState = PlayerState.FREEFALL;
                } else if (rb.velocity.x == 0) {
                    nextState = PlayerState.IDLE;
                }
                if (shootTimer + shootTime < Time.time) {
                    if (right) {
                        sprite.transform.localScale = rightScale;
                    } else {
                        sprite.transform.localScale = leftScale;
                    }
                }
                break;
            case PlayerState.JUMPING:
                if (rb.velocity.y < 0) {
                    nextState = PlayerState.FREEFALL;
                }
                if (shootTimer + shootTime < Time.time) {
                    if (right) {
                        sprite.transform.localScale = rightScale;
                    } else {
                        sprite.transform.localScale = leftScale;
                    }
                }
                break;
            case PlayerState.FREEFALL:
                if (rb.velocity.y < 0) {
                    nextState = PlayerState.FREEFALL;
                } else if (rb.velocity.x != 0) {
                    nextState = PlayerState.WALKING;
                } else if (rb.velocity.x == 0) {
                    nextState = PlayerState.IDLE;
                }
                if (shootTimer + shootTime < Time.time) {
                    if (right) {
                        sprite.transform.localScale = rightScale;
                    } else {
                        sprite.transform.localScale = leftScale;
                    }
                }
                break;
        }

        // State transition resolution and animation triggers
        if (nextState != state) {
            state = nextState;
            if (shootTimer + shootTime < Time.time) {
                switch (state) {
                    case PlayerState.IDLE:
                        if (right) {
                            anim.SetTrigger("idle_R");
                        } else {
                            anim.SetTrigger("idle_L");
                        }
                        break;
                    case PlayerState.WALKING:
                        anim.SetTrigger("walk");
                        break;
                    case PlayerState.JUMPING:
                        anim.SetTrigger("jump");
                        break;
                }
            }
        }
    }

    public void Land() {
        // To be called by falling hitbox
        if (shootTimer + shootTime < Time.time) {
            if (state == PlayerState.JUMPING || state == PlayerState.FREEFALL) {
                if (rb.velocity.x != 0) {
                    anim.SetTrigger("walk");
                } else {
                    state = PlayerState.IDLE;
                    if (right) {
                        anim.SetTrigger("idle_R");
                    } else {
                        anim.SetTrigger("idle_L");
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        float inX = 0;
        if (checkInput(restrictions[currentInputSet].left)) inX += -1;
        if (checkInput(restrictions[currentInputSet].right)) inX += 1;
        float dx = speed * inX;

        Vector2 v = rb.velocity;
        float inY = checkInput(restrictions[currentInputSet].jump) ? 1 : 0;
        if (inY > 0 && (state == PlayerState.IDLE || state == PlayerState.WALKING)) {
            v = rb.velocity;
            v.y = jumpPower;
            rb.velocity = v;
        }

        v = rb.velocity;
        v.x = dragX * v.x + dx;
        rb.velocity = v;

        if (rb.velocity.x < 0) right = false;
        if (rb.velocity.x > 0) right = true;
    }

    public void Respawn() {
        gameObject.transform.position = RespawnPoint;
    }
}
