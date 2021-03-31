using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] playList;
    public AudioSource source;
    
    private AudioClip currentClip;
    private int currentClipIndex;

    public static MusicPlayer instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);

        source.clip = playList[0];

        currentClip = source.clip;
        currentClipIndex = 0;

        source.Play();

        StartCoroutine(ReadSongPlayback());
    }

    private IEnumerator ReadSongPlayback()
    {
        yield return new WaitForSeconds(currentClip.length);

        source.Stop();

        currentClipIndex++;

        source.clip = playList[currentClipIndex];
        currentClip = source.clip;

        source.Play();
    }
}
