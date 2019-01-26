using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const string inputHorizontalMovement = "Horizontal Movement";
    const string inputVerticalMovement = "Vertical Movement";

    Rewired.Player player;
    float horizontalAxis = 0f;
    float verticalAxis = 0f;

    // Attributes
    float speed = 10f;

    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetSystemPlayer();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalAxis = player.GetAxis(inputHorizontalMovement);
        verticalAxis = player.GetAxis(inputVerticalMovement);

        if (horizontalAxis != 0)
            Debug.Log(horizontalAxis);

        if (verticalAxis != 0)
            Debug.Log(verticalAxis);

        body.velocity = new Vector2(horizontalAxis, verticalAxis).normalized * speed;
    }
}
