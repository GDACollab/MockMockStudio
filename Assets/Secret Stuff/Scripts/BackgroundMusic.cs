using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("playMusic");
    }

    IEnumerator playMusic()
    {
        yield return new WaitForSeconds(1.0f);
        AudioClip backgroundMusic = GetComponent<CoreAssetLoader>().backgroundMusic.sound;
        AudioSource x = this.AddComponent<AudioSource>();
        x.clip = backgroundMusic;
        x.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
