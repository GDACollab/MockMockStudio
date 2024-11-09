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
    public void EnemyUpdateUhhh()
    {
        enemyRB.gravityScale = -1;
        enemyRB.MovePosition(new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f)));
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
        list.Add(EnemyUpdate_Example_B);
        list.Add(EnemyUpdateUhhh);
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
