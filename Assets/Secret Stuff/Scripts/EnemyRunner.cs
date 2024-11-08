using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunner : MonoBehaviour
{
    EnemyLogic enemyLogic;
    LoadAssetFromCore core;

    // Start is called before the first frame update
    void Start()
    {
        GameObject core = GameObject.Find("Core");
        if (core == null) Debug.LogError("Missing core in scene!");
        CoreAssetLoader x = core.GetComponent<CoreAssetLoader>();

        enemyLogic = x.enemyScript.script;
        Debug.Log(x.enemyScript.name);

        enemyLogic.onStart();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyLogic)
        {
            enemyLogic.onUpdate();
        }
    }


}
