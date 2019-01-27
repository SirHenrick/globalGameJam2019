using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggo : MonoBehaviour
{
    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        var startingDirection = new Vector2(Random.value, Random.value);
        startingDirection.Normalize();

        body.velocity = startingDirection * 5;

    }

    // Update is called once per frame
    void Update()
    {
    }
}
