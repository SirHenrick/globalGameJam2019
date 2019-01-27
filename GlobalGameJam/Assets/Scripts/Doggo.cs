using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggo : MonoBehaviour
{
    private Rigidbody2D body;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        var startingDirection = new Vector2(Random.value, Random.value);
        startingDirection.Normalize();

        body.velocity = startingDirection * 5;

    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.transform.localScale =  new Vector2(Mathf.Sign(body.velocity.x), 1f);
    }
}
