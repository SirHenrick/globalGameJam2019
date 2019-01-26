using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    List<Ingredient> ingredients;
    List<Recipe> recipes;

    float cookTimer = 0f;
    float cookDuration = 2f;

    void Start()
    {
        recipes = new List<Recipe>()
        {
            Recipes.instance.friedEgg
        };
    }

    void Update()
    {
        if (cookTimer <= 0)
        {

        }

        cookTimer -= Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var ingredient = collision.gameObject.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            ingredients.Add(ingredient);
            cookTimer = cookDuration;
        }
    }
}
