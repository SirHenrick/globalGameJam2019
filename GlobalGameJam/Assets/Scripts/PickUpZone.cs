using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpZone : MonoBehaviour
{
    public LayerMask itemLayerMask;
    public LayerMask cabinetLayerMask;
    public Transform player;

    public GameObject NearestItem { get; private set; } = null;

    void Update()
    {
        Collider2D[] itemHitColliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0, itemLayerMask);
        Collider2D[] cabinetHitColliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0, cabinetLayerMask);

        if (itemHitColliders.Length > 0)
            NearestItem = GetNearestItem(itemHitColliders);
        else if (cabinetHitColliders.Length > 0)
            NearestItem = GetNearestItem(cabinetHitColliders);
        else NearestItem = null;

        var facing = player.GetComponent<Player>().Facing;
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
