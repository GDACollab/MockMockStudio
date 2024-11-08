using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DemoEnemy : EnemyLogic
{
    int number = 0;
    public override void onStart() {
        Debug.Log("Started!");
    }
    public override void onUpdate() {
        if (number == 0)
        {
            number += 1;
            Debug.Log("Updated!");
        }
    }
    public override void onDestroy() {
        Debug.Log("Destroyed!");
    }
}
