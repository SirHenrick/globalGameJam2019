using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{

    public AudioSource sfxPlayer;
    public AudioClip scoreSFX;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var fulfilled = Recipes.instance.FulfillOrder(collision.gameObject.tag);

        sfxPlayer.PlayOneShot(scoreSFX);

        Destroy(collision.gameObject);
    }
}
