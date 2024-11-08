using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CoreAssetLoader : MonoBehaviour
{
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
        public EnemyLogic script;
    }

    [HideInInspector]
    public SpriteLoaded playerSprite;
    public SpriteLoaded enemySprite;
    public SpriteLoaded tilesetSprite;
    public SpriteLoaded noteSprite;
    public SpriteLoaded backgroundSprite;

    [HideInInspector]
    public SoundLoaded walkSound;
    public SoundLoaded backgroundMusic;
    public SoundLoaded jumpSound;
    public SoundLoaded paperPickupSound;
    public SoundLoaded takeDamageSound;
    public SoundLoaded enemyBarkSound;

    [HideInInspector]
    public ScriptLoaded enemyScript;

    void Awake()
    {
        // Get Player sprites
        getRandomSprite(ref playerSprite,"Assets/CONTENT/Art/Player");
        getRandomSprite(ref enemySprite, "Assets/CONTENT/Art/Enemy");
        getRandomSprite(ref tilesetSprite, "Assets/CONTENT/Art/Tilesets");
        getRandomSprite(ref noteSprite, "Assets/CONTENT/Art/Notes");
        getRandomSprite(ref backgroundSprite, "Assets/CONTENT/Art/Background");

        // Get Sound effects
        getRandomSound(ref walkSound, "Assets/CONTENT/Sound/Walk");
        getRandomSound(ref backgroundMusic, "Assets/CONTENT/Sound/Background Music");
        getRandomSound(ref jumpSound, "Assets/CONTENT/Sound/Jump");
        getRandomSound(ref paperPickupSound, "Assets/CONTENT/Sound/Pickup Paper");
        getRandomSound(ref takeDamageSound, "Assets/CONTENT/Sound/Take Damage");

        // Get programming
        getRandomScript(ref enemyScript, "Assets/CONTENT/Programming");
    }

    void getRandomScript(ref ScriptLoaded outScript, string filePath)
    {
        var possibleScripts = new Dictionary<String, EnemyLogic>();

        foreach (var script in AssetDatabase.FindAssets("t:Script", new[] { filePath }))
        {
            var pathToSprite = AssetDatabase.GUIDToAssetPath(script);
            UnityEngine.Object foundObject = AssetDatabase.LoadAssetAtPath(pathToSprite, typeof(EnemyLogic));
            String name = Path.GetFileNameWithoutExtension(pathToSprite);
            possibleScripts.Add(name, (EnemyLogic)foundObject);
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
