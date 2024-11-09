using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        Player player;
        if (other.TryGetComponent(out player)){
            player.KillPlayer();
        }
    }
}
