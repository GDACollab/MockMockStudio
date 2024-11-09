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


    // TateM-Prog-EnemyScript
    private float tmAngle = 0; //angle in radians
    private float tmRadius = 2;
    private float tmTimer = 0;
    private float tmX = 0;
    private float tmY = 0;
    private bool tmAttacking = false;
    public void Tate_Monster()
    {
        enemyRB.gravityScale = 0;

        // update angle
        tmAngle += 1 * Time.deltaTime;
        if (tmAngle >= Mathf.PI)
        {
            tmAngle -= 2 * Mathf.PI;
        }

        if (!tmAttacking)
        {
            // update timer
            tmTimer += Time.deltaTime;
            if (tmTimer > 3)
            {
                print(tmTimer);
                tmAttacking = true;
                tmTimer = 0;
            }

            // calculate positions
            tmX = player.transform.position.x + tmRadius * Mathf.Cos(tmAngle);
            tmY = player.transform.position.y + tmRadius * Mathf.Sin(tmAngle);

            // update position
            Vector2 position = new Vector2(tmX, tmY);
            enemyRB.MovePosition(position);
        }
        else
        {
            // change radius
            if (tmRadius > -2)
            {
                tmRadius -= 0.1f * Time.deltaTime;
            }
            else
            {
                tmRadius = 2;
                tmAngle += Mathf.PI;
                tmAttacking = false;
            }

            // calculate positions
            tmX = player.transform.position.x + tmRadius * Mathf.Cos(tmAngle);
            tmY = player.transform.position.y + tmRadius * Mathf.Sin(tmAngle);

            // update position
            Vector2 position = new Vector2(tmX, tmY);
            enemyRB.MovePosition(position);
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
        list.Add(Tate_Monster);
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
