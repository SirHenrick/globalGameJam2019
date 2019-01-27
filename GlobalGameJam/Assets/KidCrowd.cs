using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidCrowd : MonoBehaviour
{
    float timer = 0;
    public float timeLimit = 0.2f;
    bool state = false;

    public SpriteRenderer spriteRenderer;

    public Sprite firstSprite;
    public Sprite secondSprite;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeLimit)
        {
            timer = 0;
            state = !state;
        }

        if (state)
        {
            spriteRenderer.sprite = firstSprite;
        }else
        {
            spriteRenderer.sprite = secondSprite;
        }

    }
}
