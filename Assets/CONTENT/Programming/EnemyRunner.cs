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

    //-------- Tyler Variables --------
    private const float dashSpeedTyler = 10f;
    private const float dashTimeWaitTyler = 2f; //Seconds
    private const float dashTimePlusMinusTyler = 0.5f; //Seconds
    private const float frictionPerFrameTyler = 0.98f;
    private float distanceStartSpeedingUpTyler = 7.5f;
    private float dashScaleMultiplierTyler = 0.7f;
    private float passiveScaleMultiplierTyler = 1.005f;

    private LayerMask groundLayerTyler;
    private float dashTimerTyler = 1f;
    private float dashTimeWaitThisTimeTyler = 2f;
    //-------- Tyler Variables --------



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

    //-------- Tyler Function --------
    public void EnemyUpdate_Example_Tyler()
    {
        groundLayerTyler = LayerMask.GetMask("Ground");
        enemyRB.gravityScale = 0;

        if (
            Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundLayerTyler) == true
            || Physics2D.Raycast(transform.position, Vector2.right, 0.5f, groundLayerTyler) == true
            || Physics2D.Raycast(transform.position, Vector2.left, 0.5f, groundLayerTyler) == true
        )
        {
            enemyRB.velocity = Vector3.up * dashSpeedTyler;
            dashTimerTyler = 0f;
            return;
        }

        float distanceScaler = Vector3.Distance(transform.position, player.transform.position)/distanceStartSpeedingUpTyler;
        distanceScaler = Mathf.Max(1, distanceScaler);

        if (dashTimerTyler >= dashTimeWaitThisTimeTyler)
        {
            Vector3 vectorToPlayer = player.transform.position - transform.position;
            vectorToPlayer = vectorToPlayer.normalized;
            enemyRB.velocity = vectorToPlayer * dashSpeedTyler * distanceScaler;

            dashTimerTyler -= dashTimeWaitThisTimeTyler;
            dashTimeWaitThisTimeTyler = dashTimeWaitTyler + UnityEngine.Random.Range(-1 * dashTimePlusMinusTyler, dashTimePlusMinusTyler);

            transform.localScale = new Vector3(transform.localScale.x * dashScaleMultiplierTyler, transform.localScale.y * dashScaleMultiplierTyler, transform.localScale.z * dashScaleMultiplierTyler);
        }
        else
        {
            if (transform.localScale.x < 1)
            {
                transform.localScale = new Vector3(transform.localScale.x * passiveScaleMultiplierTyler, transform.localScale.y * passiveScaleMultiplierTyler, transform.localScale.z * passiveScaleMultiplierTyler); ;
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            dashTimerTyler += Time.deltaTime;
            enemyRB.velocity *= frictionPerFrameTyler;
        }
        
    }
    //-------- Tyler Function --------

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
        list.Add(EnemyUpdate_Example_Tyler);
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
