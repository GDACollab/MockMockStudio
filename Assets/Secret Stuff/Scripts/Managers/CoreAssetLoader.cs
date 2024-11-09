using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    public SpriteLoaded heartFullSprite;
    public SpriteLoaded heartEmptySprite;

    [HideInInspector]
    public SoundLoaded walkSound;
    public SoundLoaded backgroundMusic;
    public SoundLoaded jumpSound;
    public SoundLoaded paperPickupSound;
    public SoundLoaded takeDamageSound;
    public SoundLoaded enemyBarkSound;

    [HideInInspector]
    public ScriptLoaded enemyScript;

    private Sprite centerTile;
    private Sprite cornerTile;
    private Sprite sideTile;
    private Sprite sideTwoTile;
    private Sprite sideThreeTile;
    private Sprite sideFourTile;

    public RuleTile tile;
    public Tilemap tilemap;

    void Awake()
    {
        tilemap = GameObject.FindWithTag("Ground").GetComponentInChildren<Tilemap>();
        
        // Get Player sprites
        getRandomSprite(ref playerSprite,"Assets/CONTENT/Art/Player");
        getRandomSprite(ref enemySprite, "Assets/CONTENT/Art/Enemy");
        getRandomSprite(ref noteSprite, "Assets/CONTENT/Art/Notes");
        getRandomSprite(ref backgroundSprite, "Assets/CONTENT/Art/Background");
        getRandomHeartSprites(ref heartFullSprite, ref heartEmptySprite, "Assets/CONTENT/Art/Heart");

        // Get Sound effects
        getRandomSound(ref walkSound, "Assets/CONTENT/Sound/Walk");
        getRandomSound(ref backgroundMusic, "Assets/CONTENT/Sound/Background Music");
        getRandomSound(ref jumpSound, "Assets/CONTENT/Sound/Jump");
        getRandomSound(ref paperPickupSound, "Assets/CONTENT/Sound/Pickup Paper");
        getRandomSound(ref takeDamageSound, "Assets/CONTENT/Sound/Take Damage");

        // Get programming
        getRandomScript(ref enemyScript, "Assets/CONTENT/Programming");

        string[] folders = AssetDatabase.GetSubFolders("Assets/CONTENT/Art/Tilesets");
        int folderX = UnityEngine.Random.Range(0, folders.Length);

        string[] sprite = AssetDatabase.FindAssets("Corner_ t:Sprite", new[] { folders[folderX] });
        var pathToSprite = AssetDatabase.GUIDToAssetPath(sprite[0]);
        Sprite spriteFound = (Sprite)AssetDatabase.LoadAssetAtPath(pathToSprite, typeof(Sprite));
        cornerTile = spriteFound;

        sprite = AssetDatabase.FindAssets("Middle_ t:Sprite", new[] { folders[folderX] });
        pathToSprite = AssetDatabase.GUIDToAssetPath(sprite[0]);
        spriteFound = (Sprite)AssetDatabase.LoadAssetAtPath(pathToSprite, typeof(Sprite));
        centerTile = spriteFound;

        sprite = AssetDatabase.FindAssets("Side2_ t:Sprite", new[] { folders[folderX] });
        pathToSprite = AssetDatabase.GUIDToAssetPath(sprite[0]);
        spriteFound = (Sprite)AssetDatabase.LoadAssetAtPath(pathToSprite, typeof(Sprite));
        sideTwoTile = spriteFound;

        sprite = AssetDatabase.FindAssets("Side3_ t:Sprite", new[] { folders[folderX] });
        pathToSprite = AssetDatabase.GUIDToAssetPath(sprite[0]);
        spriteFound = (Sprite)AssetDatabase.LoadAssetAtPath(pathToSprite, typeof(Sprite));
        sideThreeTile = spriteFound;

        sprite = AssetDatabase.FindAssets("Side4_ t:Sprite", new[] { folders[folderX] });
        pathToSprite = AssetDatabase.GUIDToAssetPath(sprite[0]);
        spriteFound = (Sprite)AssetDatabase.LoadAssetAtPath(pathToSprite, typeof(Sprite));
        sideFourTile = spriteFound;

        sprite = AssetDatabase.FindAssets("Side_ t:Sprite", new[] { folders[folderX] });
        pathToSprite = AssetDatabase.GUIDToAssetPath(sprite[0]);
        spriteFound = (Sprite)AssetDatabase.LoadAssetAtPath(pathToSprite, typeof(Sprite));
        sideTile = spriteFound;

        Sprite[] toAdd = { sideThreeTile, cornerTile, sideTile, sideTwoTile, sideFourTile };

        int i = 0;
        tile.m_DefaultSprite = centerTile;
        foreach (var tilerule in tile.m_TilingRules)
        {
            if (i < toAdd.Length)
            {
                Sprite[] arrr = { toAdd[i] };
                tilerule.m_Sprites = arrr;
                i++;
            }
        }
        tilemap.RefreshAllTiles();
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
    
    void getRandomHeartSprites(ref SpriteLoaded outSprite1, ref SpriteLoaded outSprite2, string filePath)
    {
        var possibleSprites = new Dictionary<String,Sprite>();
        
        string[] folders = AssetDatabase.GetSubFolders(filePath);
        
        if (folders.Length == 0){
            Debug.LogError("No heart folders found under" + filePath);
            return;
        }
        
        int x = UnityEngine.Random.Range(0, folders.Length);
        string folderPath = folders[x];
        string[] assets = AssetDatabase.FindAssets("t:Sprite", new[] { folderPath });
        
        if (assets.Length == 0){
            Debug.LogError("No heart sprites found under" + folderPath);
            return;
        }
        else if (assets.Length < 2){
            Debug.LogWarning("Not enough heart sprites found under" + folderPath);
            var pathToSprite = AssetDatabase.GUIDToAssetPath(assets[0]);
            UnityEngine.Object foundObject = AssetDatabase.LoadAssetAtPath(pathToSprite, typeof(Sprite));
            String name = Path.GetFileNameWithoutExtension(pathToSprite);
            possibleSprites.Add(name, (Sprite)foundObject);
        }
        else{
            foreach (string file in assets){
                var pathToSprite = AssetDatabase.GUIDToAssetPath(file);
                if (Regex.Match(pathToSprite.ToLower(), @"\(full\)").Success){
                    UnityEngine.Object foundObject = AssetDatabase.LoadAssetAtPath(pathToSprite, typeof(Sprite));
                    outSprite1.sprite = (Sprite)foundObject;
                    outSprite1.name = Path.GetFileNameWithoutExtension(pathToSprite);
                }
                else if(Regex.Match(pathToSprite.ToLower(), @"\(empty\)").Success){
                    UnityEngine.Object foundObject = AssetDatabase.LoadAssetAtPath(pathToSprite, typeof(Sprite));
                    outSprite2.sprite = (Sprite)foundObject;
                    outSprite2.name = Path.GetFileNameWithoutExtension(pathToSprite);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
