using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioPlayer;
    public bool clip2Played;
    public AudioClip clip2;
    public AudioClip spedUp;
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioPlayer.isPlaying)
        {
            if (!clip2Played)
            {
                audioPlayer.clip = clip2;
                audioPlayer.loop = true;
                audioPlayer.Play();
                clip2Played = true;
            }
        }
    }
}
