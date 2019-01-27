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
    private bool isThrowing;
    private int currentThrowFrame;
    Recipe garbageRecipe;
    public GameObject HeldItem { get; private set; } = null;
    public Vector2 Facing { get; private set; } = new Vector2(0, -1);

    // Attributes
    float speed = 7f;
    float pickUpDistance = 1f;
    float throwForce = 3f;
    public SpriteRenderer spriteRenderer;
    public PickUpZone pickUpZone;
    public GameObject throwEffect;
    public List<Sprite> throwAnimation;
    
    Rigidbody2D body;

    private Animator animator;

    private SpriteRenderer throwSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        garbageRecipe = Recipes.instance.garbage;

        player = ReInput.players.GetSystemPlayer();
        body = GetComponent<Rigidbody2D>();
        animator = spriteRenderer.GetComponent<Animator>();
        throwSpriteRenderer = throwEffect.GetComponent<SpriteRenderer>();
        currentThrowFrame = throwAnimation.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {    
        horizontalAxis = player.GetAxis(inputHorizontalMovement);
        verticalAxis = player.GetAxis(inputVerticalMovement);

        pressedPickUp = player.GetButtonDown(inputPickUp);

        var direction = new Vector2(horizontalAxis, verticalAxis).normalized;
        body.velocity = direction * speed;

        if (direction.magnitude <= Mathf.Epsilon)
        {
            animator.SetFloat("animationSpeed", 0f);
        }
        else
        {
            animator.SetFloat("animationSpeed", 1f);
        }
        
        if (Mathf.Abs(direction.x) > Mathf.Epsilon)
        {
            spriteRenderer.transform.localScale =  new Vector2(Mathf.Sign(direction.x), 1f);
        }
        animator.SetBool("isWalking", direction.x >= Mathf.Epsilon || direction.x <= -Mathf.Epsilon);
        animator.SetBool("isWalkingUp", direction.y >= Mathf.Epsilon);
        animator.SetBool("isWalkingDown", direction.y <= -Mathf.Epsilon);

        if (!direction.Equals(Vector2.zero))
            Facing = direction;

        pickUpZone.transform.localPosition = Facing * pickUpDistance;

        if (HeldItem != null)
        {
            HeldItem.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
            HeldItem.GetComponent<SpriteRenderer>().sortingLayerName = "Held";

            if (player.GetButtonDown(inputPickUp))
            {
                isThrowing = true;
                currentThrowFrame = 0;
                var angle = Mathf.Atan2(Facing.y, Facing.x) * Mathf.Rad2Deg - 90;
                throwEffect.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
        animator.SetBool("isCarrying", HeldItem != null);

        throwEffect.transform.localPosition = pickUpZone.transform.localPosition;
    }

    private void FixedUpdate()
    {
        if (isThrowing)
        {
            if (currentThrowFrame == throwAnimation.Count - 1)
            {
                isThrowing = false;
            }
            else
            {
                currentThrowFrame += 1;
                throwSpriteRenderer.sprite = throwAnimation[currentThrowFrame];
            }
        }
        throwSpriteRenderer.sprite = throwAnimation[currentThrowFrame];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var dish = Instantiate(garbageRecipe.result);
        dish.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
        dish.GetComponent<Rigidbody2D>().AddForce(Vector2.up * .75f, ForceMode2D.Impulse);
    }
}
