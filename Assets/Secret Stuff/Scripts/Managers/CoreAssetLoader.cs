using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CoreAssetLoader : MonoBehaviour
{

    /*
     * 
     * 
     * TODO on the 8th:
     * - Make a script generic for enemy behaviour
     * - Add all things that can load assets
     * - Add enemy temp behavior and example sounds
     * 
     * 
     * Every play we must load a:
     * 
     * Sprites for:
     * - Tileset, player, enemy, paper note, background sprite
     * 
     * Sounds for:
     * - Jumping, Moving, Taking Damage, Paper Pickup, Random enemy mob noises, msuic track
     * 
     * Enemy Script:
     * - Script
     * 
     * AND provide creditation, name is filename?
     * 
     * 
     * Need to scan folder for all assets, then choose one, 
     */


    public struct SpriteLoaded
    {
        public string name;
        public Sprite sprite;
    }

    public struct SoundLoaded
    {
        public string name;
        public AudioClip sound;
    }

    public struct ScriptLoaded
    {
        public string name;
        public MonoBehaviour script;
    }

    [HideInInspector]
    public SpriteLoaded playerSprite;

    [HideInInspector]
    public SoundLoaded walkSound;

    [HideInInspector]
    public ScriptLoaded enemyScript;


    void Awake()
    {
        // Find our player sprites
        getRandomSprite(ref playerSprite,"Assets/CONTENT/Art");
        getRandomSound(ref walkSound, "Assets/CONTENT/Sound");
        getRandomScript(ref enemyScript, "Assets/CONTENT/Programming");
    }

    void getRandomScript(ref ScriptLoaded outScript, string filePath)
    {
        var possibleScripts = new Dictionary<String, MonoBehaviour>();

        foreach (var script in AssetDatabase.FindAssets("t:Script", new[] { filePath }))
        {
            var pathToSprite = AssetDatabase.GUIDToAssetPath(script);
            UnityEngine.Object foundObject = AssetDatabase.LoadAssetAtPath(pathToSprite, typeof(MonoBehaviour));
            String name = Path.GetFileNameWithoutExtension(pathToSprite);
            possibleScripts.Add(name, (MonoBehaviour)foundObject);
        }

        if (possibleScripts.Count == 0)
        {
            Debug.LogError("No Scripts in folder " + filePath);
            return;
        }

        // Choose random sprite
        int x = UnityEngine.Random.Range(0, possibleScripts.Count);
        outScript.script = possibleScripts.ElementAt(x).Value;
        outScript.name = possibleScripts.ElementAt(x).Key;
    }

    void getRandomSound(ref SoundLoaded outSound, string filePath)
    {
        var possibleSounds = new Dictionary<String, AudioClip>();

        foreach (var audio in AssetDatabase.FindAssets("t:AudioClip", new[] { filePath }))
        {
            var pathToSprite = AssetDatabase.GUIDToAssetPath(audio);
            UnityEngine.Object foundObject = AssetDatabase.LoadAssetAtPath(pathToSprite, typeof(AudioClip));
            String name = Path.GetFileNameWithoutExtension(pathToSprite);
            possibleSounds.Add(name, (AudioClip)foundObject);
        }

        if (possibleSounds.Count == 0)
        {
            Debug.LogError("No Sounds in folder " + filePath);
            return;
        }

        // Choose random sprite
        int x = UnityEngine.Random.Range(0, possibleSounds.Count);
        outSound.sound = possibleSounds.ElementAt(x).Value;
        outSound.name = possibleSounds.ElementAt(x).Key;
    }

    void getRandomSprite(ref SpriteLoaded outSprite, string filePath)
    {
        var possibleSprites = new Dictionary<String,Sprite>();

        foreach (var sprite in AssetDatabase.FindAssets("t:Sprite", new[] { filePath }))
        {
            var pathToSprite = AssetDatabase.GUIDToAssetPath(sprite);
            UnityEngine.Object foundObject = AssetDatabase.LoadAssetAtPath(pathToSprite, typeof(Sprite));
            String name = Path.GetFileNameWithoutExtension(pathToSprite);
            possibleSprites.Add(name, (Sprite)foundObject);
        }

        if (possibleSprites.Count == 0)
        {
            Debug.LogError("No Sprites in folder " + filePath);
            return;
        }

        // Choose random sprite
        int x = UnityEngine.Random.Range(0, possibleSprites.Count);
        outSprite.sprite = possibleSprites.ElementAt(x).Value;
        outSprite.name = possibleSprites.ElementAt(x).Key;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
