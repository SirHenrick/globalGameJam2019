using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const string inputHorizontalMovement = "Horizontal Movement";
    const string inputVerticalMovement = "Vertical Movement";
    const string inputPickUp = "Pick Up";

    Rewired.Player player;
    float horizontalAxis = 0f;
    float verticalAxis = 0f;
    bool pressedPickUp = false;
    bool hasItem = false;
    Vector2 facing = Vector2.zero;

    // Attributes
    float speed = 5f;
    float pickUpDistance = 1f;

    public SpriteRenderer spriteRenderer;
    public Transform pickUpZone;

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

        pressedPickUp = player.GetButtonDown(inputPickUp);

        var direction = new Vector2(horizontalAxis, verticalAxis).normalized;
        body.velocity = direction * speed;

        if (!direction.Equals(Vector2.zero))
            facing = direction;

        pickUpZone.localPosition = facing * pickUpDistance;
    }

}
