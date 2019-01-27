using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var fulfilled = Recipes.instance.FulfillOrder(collision.gameObject.tag);

        Destroy(collision.gameObject);
    }
}
