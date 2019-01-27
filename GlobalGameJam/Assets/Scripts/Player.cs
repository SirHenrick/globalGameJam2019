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
    public GameObject HeldItem { get; private set; } = null;
    public Vector2 Facing { get; private set; } = new Vector2(0, -1);

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
            Facing = direction;

        pickUpZone.transform.localPosition = Facing * pickUpDistance;

        if (HeldItem != null)
        {
            HeldItem.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
            HeldItem.GetComponent<SpriteRenderer>().sortingLayerName = "Held";

            if (player.GetButtonDown(inputPickUp))
            {
                HeldItem.transform.position = pickUpZone.transform.position;
                HeldItem.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                HeldItem.GetComponent<Rigidbody2D>().AddForce(Facing * throwForce, ForceMode2D.Impulse);
                HeldItem.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                HeldItem = null;
            }
        }
        else if (HeldItem == null && pickUpZone.NearestItem != null && player.GetButtonDown(inputPickUp))
        {
            var cabinet = pickUpZone.NearestItem.GetComponent<Cabinet>();
            if (cabinet != null)
            {
                var newItem = Instantiate(cabinet.resource);
                HeldItem = newItem;
                HeldItem.transform.position = pickUpZone.transform.position;
            }
            else HeldItem = pickUpZone.NearestItem;
        }
    }
}
