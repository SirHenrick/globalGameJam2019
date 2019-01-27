using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    List<string> cookingIngredients;
    List<Recipe> recipes;
    Recipe garbageRecipe;
    Sprite originalSprite;
    SpriteRenderer spriteRenderer;
    float cookTimer = 0f;

    // Attributes
    float cookDuration = 2f;
    float creationSpeed = .75f;
    float startOffset = 1;
    public Sprite on;

    void Start()
    {
        cookingIngredients = new List<string>();
        recipes = new List<Recipe>()
        {
            Recipes.instance.friedEgg,
            Recipes.instance.pancakes,
            Recipes.instance.frenchToast,
            Recipes.instance.cake
        };

        garbageRecipe = Recipes.instance.garbage;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    void Update()
    {
        if (cookingIngredients.Count > 0)
            spriteRenderer.sprite = on;
        else
            spriteRenderer.sprite = originalSprite;

        if (cookingIngredients.Count > 0 && cookTimer <= 0)
        {

            var equal = false;
            Recipe finalRecipe = garbageRecipe;
            foreach (var recipe in recipes)
            {
                
                if (recipe.ingredients.Count == cookingIngredients.Count)
                {
                    equal = true;
                    foreach (var ingredient in recipe.ingredients)
                    {
                        if (!cookingIngredients.Contains(ingredient.tag))
                            equal = false;

                        finalRecipe = recipe;
                    }
                }
            }

            if (equal)
            {
                var dish = Instantiate(finalRecipe.result);
                dish.transform.position = new Vector2(transform.position.x, transform.position.y - startOffset);
                dish.GetComponent<Rigidbody2D>().AddForce(Vector2.down * creationSpeed, ForceMode2D.Impulse);
            }
            else
            {
                var dish = Instantiate(garbageRecipe.result);
                dish.transform.position = new Vector2(transform.position.x, transform.position.y - startOffset);
                dish.GetComponent<Rigidbody2D>().AddForce(Vector2.down * creationSpeed, ForceMode2D.Impulse);
            }

            cookingIngredients = new List<string>();
        }

        cookTimer -= Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var ingredient = collision.gameObject.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            cookingIngredients.Add(ingredient.tag);
            cookTimer = cookDuration;

            Destroy(ingredient.gameObject);
        }
    }
}
