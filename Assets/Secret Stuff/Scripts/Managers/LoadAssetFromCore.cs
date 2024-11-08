using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoreAssetLoader;

public class LoadAssetFromCore : MonoBehaviour
{
    public enum LOADABLE_SPRITES
    {
        NONE,SPR_PLAYER,SPR_ENEMY,SPR_TILESET,SPR_NOTE,SPR_BACKGROUND
    }
    public enum LOADABLE_SOUNDS
    {
        NONE, SFX_WALK, SFX_BACKGROUND, SFX_JUMP, SFX_PICKUP, SFX_TAKEDAMAGE, SFX_ENEMY
    }

    SpriteRenderer sprite;
    AudioSource source;
    public EnemyLogic script;

    public LOADABLE_SPRITES spriteToLoad;
    public LOADABLE_SOUNDS soundToLoad;
    public bool loadScript;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject core = GameObject.Find("Core");
        if (core == null) Debug.LogError("Missing core in scene!");
        CoreAssetLoader x = core.GetComponent<CoreAssetLoader>();

        if (spriteToLoad != LOADABLE_SPRITES.NONE)
        {
            sprite = GetComponent<SpriteRenderer>();

            switch (spriteToLoad)
            {
                case LOADABLE_SPRITES.SPR_PLAYER:
                    sprite.sprite = x.playerSprite.sprite;
                    break;
                case LOADABLE_SPRITES.SPR_TILESET:
                    sprite.sprite = x.tilesetSprite.sprite;
                    break;
                case LOADABLE_SPRITES.SPR_ENEMY:
                    sprite.sprite = x.enemySprite.sprite;
                    break;
                case LOADABLE_SPRITES.SPR_NOTE:
                    sprite.sprite = x.noteSprite.sprite;
                    break;
                case LOADABLE_SPRITES.SPR_BACKGROUND:
                    sprite.sprite = x.backgroundSprite.sprite;
                    break;
            }

        }

        if (soundToLoad != LOADABLE_SOUNDS.NONE)
        {
            source = GetComponent<AudioSource>();

            switch (soundToLoad)
            {
                case LOADABLE_SOUNDS.SFX_WALK:
                    source.clip = x.walkSound.sound;
                    break;
                case LOADABLE_SOUNDS.SFX_BACKGROUND:
                    source.clip = x.backgroundMusic.sound;
                    break;
                case LOADABLE_SOUNDS.SFX_JUMP:
                    source.clip = x.jumpSound.sound;
                    break;
                case LOADABLE_SOUNDS.SFX_PICKUP:
                    source.clip = x.paperPickupSound.sound;
                    break;
                case LOADABLE_SOUNDS.SFX_TAKEDAMAGE:
                    source.clip = x.takeDamageSound.sound;
                    break;
                case LOADABLE_SOUNDS.SFX_ENEMY:
                    source.clip = x.enemyBarkSound.sound;
                    break;
            }
        }

        if (loadScript)
        {
            script = x.enemyScript.script;
            Debug.Log("Loaded: " + x.enemyScript.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
