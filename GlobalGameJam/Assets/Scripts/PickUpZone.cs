using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpZone : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float spriteTimer = 0f;

    // Attributes
    const float grabSize = 2f;

    public LayerMask itemLayerMask;
    public LayerMask cabinetLayerMask;
    public LayerMask resultLayerMask;
    public Transform player;

    public GameObject NearestItem { get; private set; } = null;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Collider2D[] itemHitColliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale * grabSize, 0, itemLayerMask);
        Collider2D[] cabinetHitColliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale * grabSize, 0, cabinetLayerMask);
        Collider2D[] resultHitColliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale * grabSize, 0, resultLayerMask);

        if (resultHitColliders.Length > 0)
            NearestItem = GetNearestItem(resultHitColliders);
        else if (itemHitColliders.Length > 0)
            NearestItem = GetNearestItem(itemHitColliders);
        else if (cabinetHitColliders.Length > 0)
            NearestItem = GetNearestItem(cabinetHitColliders);
        else NearestItem = null;

        if (NearestItem != null && player.GetComponent<Player>().HeldItem == null)
        {
            spriteRenderer.enabled = true;

            var facing = player.GetComponent<Player>().Facing;

            if (facing.x > 0)
                transform.localScale = new Vector2(-1, 1);
            else if (facing.x < 0)
                transform.localScale = new Vector2(1, 1);
            else if (facing.y > 0)
                transform.localScale = new Vector2(1, 1);
            else transform.localScale = new Vector2(1, -1);
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }

    GameObject GetNearestItem(Collider2D[] colliders)
    {
        GameObject nearestItem = null;
        foreach (var collider in colliders)
        {
            var position = collider.transform.position;
            if (nearestItem == null)
                nearestItem = collider.gameObject;
            else if (Vector2.Distance(position, player.position) <= Vector2.Distance(nearestItem.transform.position, player.position))
                nearestItem = collider.gameObject;
        }

        return nearestItem;
    }
}
