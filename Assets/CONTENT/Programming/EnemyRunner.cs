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

    
    //ASHTON'S VARIABLES START
    float AshtonsTimer = 0;
    bool AshtonsBool = false;
    //ASHTON'S VARIABLES END


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
    //This function has the enemy walk on the ground. After a bit, it will jump up high, then launch at the player
    public void EnemyUpdate_By_Ashton()
    {
        LayerMask groundMask = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.55f, layerMask: groundMask);
        if(hit.collider == null)
        {
            enemyRB.gravityScale = 3;
            //Debug.Log("AA");
        }
        else
        {
            //Debug.Log("BB");
            enemyRB.gravityScale = 0;
            if(enemyRB.velocity.y < 0)
            {
                enemyRB.velocity = new Vector2(enemyRB.velocity.x, 0);
            }
        }
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.up, 0.55f, layerMask: groundMask);
        if(hit2.collider != null)
        {
            enemyRB.gravityScale = -3;
        }
        AshtonsTimer += Time.deltaTime;
        //enemyRB.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, 1f * Time.deltaTime));
        if (player.transform.position.x > transform.position.x)
        {
            enemyRB.velocity = new Vector2(1, enemyRB.velocity.y);
        }
        else
            enemyRB.velocity = new Vector2(-1, enemyRB.velocity.y);
        if (AshtonsTimer > 3)
        {
            if (!AshtonsBool)
            {
                //Debug.Log("JUMP");
                enemyRB.AddForce(Vector2.up * 1750);
                AshtonsBool = true;
            }
            else if(AshtonsTimer >= 3.5f)
            {
                enemyRB.AddForce(3250 * (new Vector2(player.transform.position.x - transform.position.x, (player.transform.position.y - transform.position.y) / 4).normalized));
                AshtonsTimer = 0;
                AshtonsBool = false;
            }
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
        list.Add(EnemyUpdate_By_Ashton);
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
