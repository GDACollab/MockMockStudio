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

    // START CICI CODE
    Vector3 targetPos;
    bool start = true;
    // END CICI CODE

    
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
    public void EnemyUpdate_CiCi()
    {
        if (start)
        {
            targetPos = transform.position;
            start = false;
        }

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            do {
                int distance = 5;
                float randX = UnityEngine.Random.Range(-distance, distance);
                float randY = UnityEngine.Random.Range(-distance, distance);
                Vector3 randPos = new Vector3(randX, randY, transform.position.z);
                targetPos = transform.position + randPos;
            } while (targetPos.y < -1.5f || targetPos.y > 12.0f);
            transform.position += targetPos.normalized * 0.11f;

        } else
        {
            enemyRB.MovePosition(Vector3.MoveTowards(transform.position, targetPos, 10f * Time.deltaTime));
        }
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
        list.Add(EnemyUpdate_CiCi);
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
