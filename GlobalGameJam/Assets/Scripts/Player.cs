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
    GameObject heldItem = null;
    Vector2 facing = new Vector2(0, -1);

    // Attributes
    float speed = 5f;
    float pickUpDistance = 1f;
    float throwForce = 3f;
    public SpriteRenderer spriteRenderer;
    public PickUpZone pickUpZone;

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

        pickUpZone.transform.localPosition = facing * pickUpDistance;

        if (heldItem != null)
        {
            heldItem.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
            heldItem.GetComponent<SpriteRenderer>().sortingLayerName = "Held";

            if (player.GetButtonDown(inputPickUp))
            {
                heldItem.transform.position = pickUpZone.transform.position;
                heldItem.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                heldItem.GetComponent<Rigidbody2D>().AddForce(facing * throwForce, ForceMode2D.Impulse);
                heldItem.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                heldItem = null;
            }
        }
        else if (heldItem == null && pickUpZone.NearestItem != null && player.GetButtonDown(inputPickUp))
        {
            var cabinet = pickUpZone.NearestItem.GetComponent<Cabinet>();
            if (cabinet != null)
            {
                var newItem = Instantiate(cabinet.resource);
                heldItem = newItem;
            }
            else heldItem = pickUpZone.NearestItem;

        }
    }
}
