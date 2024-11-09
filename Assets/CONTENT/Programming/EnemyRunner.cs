using System;
using System.Collections.Generic;
using UnityEngine;


public class EnemyRunner : MonoBehaviour
{
    // References 
    GameObject player;
    Rigidbody2D enemyRB;
    SpriteRenderer enemySprite;

    // Internal Logic
    Action randomAction;

    
    /*
     * Please do not modify anything but the sections between the /// comments!
     */

    /// ADD YOUR SCRIPT BELOW! ------------------------ <summary>
    /// ADD YOUR SCRIPT BELOW! ------------------------
    
    /// makes the size go crzy
    public void EnemyUpdate_Example_A()
    {
        enemyRB.gravityScale = 0;
        Vector3 small = new Vector3(UnityEngine.Random.Range(0.5f, 1.5f), UnityEngine.Random.Range(0.5f, 1.5f), 1);
        this.transform.localScale = small;
    }

    /// ADD YOUR SCRIPT ABOVE! ------------------------


    private void Start()
    {
        player = GameObject.Find("Player");
        enemyRB = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();

        var list = new List<Action>();

        /// ADD YOUR SCRIPT BELOW TO ADD!
        list.Add(EnemyUpdate_Example_A);
        /// ADD YOUR SCRIPT ABOVE TO ADD!

        var randomIndex = UnityEngine.Random.Range(0, list.Count);
        randomAction = list[randomIndex];

        // Invoke (execute) the action
        randomAction.Invoke();
    }
    private void Update()
    {
        randomAction.Invoke();

    }
}
