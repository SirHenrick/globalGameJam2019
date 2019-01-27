using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Doggo : MonoBehaviour
{    
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;

    private Animator doggoAnimator;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        doggoAnimator = GetComponent<Animator>();
        
        var animationType = Random.Range(0, 4);
        switch (animationType)
        {
            case 0 :
                doggoAnimator.SetTrigger("dogIsBrown");
                break;
            case 1 :
                doggoAnimator.SetTrigger("dogIsWhite");
                break;
            case 2 :
                doggoAnimator.SetTrigger("dogIsBlack");
                break;
            case 3 :
                doggoAnimator.SetTrigger("dogIsOrange");
                break;
        }
        
        var startingDirection = new Vector2(Random.value, Random.value);
        startingDirection.Normalize();

        body.velocity = startingDirection * 3;

    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.transform.localScale =  new Vector2(Mathf.Sign(body.velocity.x), 1f);
    }
}
