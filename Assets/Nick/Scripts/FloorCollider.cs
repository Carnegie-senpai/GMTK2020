using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FloorCollider : MonoBehaviour
{
    public PlayerController player;

    void OnCollisionEnter2D(Collision2D col) {
        player.Land();
    }
}
