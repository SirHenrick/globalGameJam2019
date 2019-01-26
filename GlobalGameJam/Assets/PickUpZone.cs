using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpZone : MonoBehaviour
{
    public LayerMask layerMask;
    public Transform player;

    public GameObject NearestItem { get; private set; } = null;

    void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0, layerMask);

        int i = 0;
        while (i < hitColliders.Length)
        {
            Debug.Log("Hit: " + hitColliders[i].name + i);
            i++;
        }

        if (hitColliders.Length > 0)
        {
            foreach (var collider in hitColliders)
            {
                var position = collider.transform.position;
                if (NearestItem == null)
                    NearestItem = collider.gameObject;
                else if (Vector2.Distance(position, player.position) >= Vector2.Distance(NearestItem.transform.position, player.position))
                    NearestItem = collider.gameObject;
            }
        }
        else NearestItem = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
