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

    public void EnemyUpdate_Matt()
    {
        GameObject enemy = GameObject.Find("Enemy");
        float newX = enemy.transform.position.x - 2f;
        Vector3 newPos = new Vector3(newX, enemy.transform.position.y, enemy.transform.position.z);

        enemyRB.gravityScale = 0;
        enemyRB.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime));

        float ranScale = UnityEngine.Random.Range(0.5f, 3f);

        Vector3 scaleChange = new Vector3(ranScale, ranScale, ranScale);

        if(player.transform.position.y > 3.5f && Input.GetKey("d"))
        {
            enemyRB.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, 50f * Time.deltaTime));
            enemy.transform.localScale = scaleChange;
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
        list.Add(EnemyUpdate_Matt);
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
