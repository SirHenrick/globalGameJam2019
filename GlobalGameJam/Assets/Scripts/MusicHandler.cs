using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public AudioSource audioPlayer;

    public AudioClip difficultyUpMusic;

    private float timer = 0;
    bool changedMusic = false;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //if (gamescore >= aCertainValue)

        if (timer > 5 && !changedMusic)
        {
            audioPlayer.clip = difficultyUpMusic;
            audioPlayer.Play();
            changedMusic = true;
        }

    }
}
