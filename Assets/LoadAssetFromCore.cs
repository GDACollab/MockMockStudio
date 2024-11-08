using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAssetFromCore : MonoBehaviour
{
    SpriteRenderer sprite;
    AudioSource source;
    MonoBehaviour script;

    public bool loadSprite;
    public bool loadSound;
    public bool loadScript;
    
    // Start is called before the first frame update
    void Start()
    {
        if (loadSprite)
        {
            sprite = GetComponent<SpriteRenderer>();
            GameObject core = GameObject.Find("Core");
            if (core == null) Debug.LogError("Missing core in scene!");
            CoreAssetLoader x = core.GetComponent<CoreAssetLoader>();

            sprite.sprite = x.playerSprite.sprite;
            Debug.Log("Loaded: " + x.playerSprite.name);
        }

        if (loadSound)
        {
            source = GetComponent<AudioSource>();
            GameObject core = GameObject.Find("Core");
            if (core == null) Debug.LogError("Missing core in scene!");
            CoreAssetLoader x = core.GetComponent<CoreAssetLoader>();

            source.clip = x.walkSound.sound;
            Debug.Log("Loaded: " + x.walkSound.name);
        }

        if (loadScript)
        {
            GameObject core = GameObject.Find("Core");
            if (core == null) Debug.LogError("Missing core in scene!");
            CoreAssetLoader x = core.GetComponent<CoreAssetLoader>();

            script = x.enemyScript.script;
            Debug.Log("Loaded: " + x.enemyScript.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
