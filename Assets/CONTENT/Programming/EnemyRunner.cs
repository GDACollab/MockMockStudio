using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class EnemyRunner : MonoBehaviour
{
    // References 
    GameObject player;
    Rigidbody2D enemyRB;
    SpriteRenderer enemySprite;
    Boolean codeRan = false;

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

    // Gabe's Code Start
    public void GabeEnemyUpdate()
    {
        // if looking at enemy, it stops
        enemyRB.gravityScale = 0;
        Boolean playerSpriteFlipped = player.GetComponentInChildren<SpriteRenderer>().flipX;

        if ((!playerSpriteFlipped && player.transform.position.x > transform.position.x) || (playerSpriteFlipped && player.transform.position.x <= transform.position.x)){
            enemyRB.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, 7f * Time.deltaTime));
        }
    }

    public void AidanEnemyUpdate(){
        if (!codeRan){
            codeRan = true;
            enemyRB.gravityScale = 0;
            StartCoroutine(teleport());
        }
    
    }

    private IEnumerator teleport(){
        float speed = 2f;
        while (true){
            yield return new WaitForSeconds(speed);
            enemyRB.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, 500f * Time.deltaTime));
            speed /= 1.14f;
        }
    }
    // Gabe's Code End
    

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
        list.Add(GabeEnemyUpdate);
        list.Add(AidanEnemyUpdate);
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
