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


    // I added this for EnemyUpdate_Mine
    float timer = 0.0f;
    // - NateWii

    // START CICI CODE
    Vector3 targetPos;
    bool start = true;
    // END CICI CODE

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


    public void EnemyUpdateUhhh()
    {
        enemyRB.gravityScale = -1;
        enemyRB.MovePosition(new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f)));
    }

    public void EnemyUpdate_CiCi()
    {
        if (start)
        {
            targetPos = transform.position;
            start = false;
        }

    public void EnemyUpdate_Attack()
    {
        enemyRB.gravityScale = 0;
        Vector3 myvector;
        myvector = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(0.0f, 2.0f), 0.0f);
        enemyRB.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position+myvector, 10f * Time.deltaTime));
    }
    public void EnemyUpdate_Hover()
    {
        enemyRB.gravityScale = 0;
        Vector3 myvector;
        myvector = new Vector3(0.0f, 2.0f, 0.0f);
        enemyRB.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position + myvector, 12.5f * Time.deltaTime));
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

    public void Liam()
    {
        enemyRB.gravityScale = UnityEngine.Random.Range(1,50);
        enemyRB.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, 2f * Time.deltaTime));
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

        } else
        {
            enemyRB.MovePosition(Vector3.MoveTowards(transform.position, targetPos, 10f * Time.deltaTime));
        }
    }

    public void EnemyUpdate_Mine() 
    {
        enemyRB.gravityScale = 0;
        enemyRB.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, 12f * Time.deltaTime));
        if (timer >= 2.0f) {
            timer = 0.0f;
            Instantiate(enemyRB, new Vector3(player.transform.position.x - 3.0f, player.transform.position.y + 1.0f, 0.0f), Quaternion.identity);
        }
        timer += Time.deltaTime;
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

        list.Add(EnemyUpdate_By_Ashton);



        list.Add(EnemyUpdate_Mine);


        list.Add(Tate_Monster);


        list.Add(EnemyUpdateUhhh);


        list.Add(EnemyUpdate_CiCi);


        list.Add(Liam);

        list.Add(EnemyUpdate_Example_Tyler);
        list.Add(EnemyUpdate_Attack);
        list.Add(EnemyUpdate_Hover);

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
