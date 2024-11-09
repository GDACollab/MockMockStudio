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

    // Added Variables
    float currentSpeed;
    
    /*
     * Please do not modify anything but the sections between the /// comments!
     */

    /// ADD YOUR SCRIPT BELOW! ------------------------
    public void EnemyUpdate_Example_A()
    {
        enemyRB.gravityScale = 0;
        enemyRB.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, 10f * Time.deltaTime));
    }
    public void EnemyUpdate_Example_B()
    {
        enemyRB.gravityScale = 0;
        enemyRB.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, 2f * Time.deltaTime));
    }

    public void EnemyUpdate_Erik()
    {
        //enemyRB.gravityScale = 0;
        //float speedRandomizer = UnityEngine.Random.Range(0.9F, 1.1F);
        //currentSpeed *= speedRandomizer;
        
        enemyRB.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime));

        int coinFlip = UnityEngine.Random.Range(0, 2);
        Vector3 scaler = new Vector3(0, 0, 0);
        int maxSize = 3;

        if (coinFlip == 0 && transform.localScale.x < maxSize)
        {
            scaler = new Vector3(0.5F, 0.5F, 0);
        }
        else if (coinFlip == 1 && transform.localScale.x >= maxSize)
        {
            scaler = new Vector3(-0.5F, -0.5F, 0);
        }

        transform.localScale += scaler;
    }

    /// ADD YOUR SCRIPT ABOVE! ------------------------


    private void Start()
    {
        player = GameObject.Find("Player");
        enemyRB = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();

        var list = new List<Action>();

        /// ADD YOUR SCRIPT BELOW TO ADD!
        //list.Add(EnemyUpdate_Example_A);
        //list.Add(EnemyUpdate_Example_B);

        enemyRB.gravityScale = 0;
        currentSpeed = 5;
        list.Add(EnemyUpdate_Erik);

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
