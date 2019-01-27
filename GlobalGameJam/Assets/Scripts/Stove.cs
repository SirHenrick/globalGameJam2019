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

    [SerializeField]
    SpriteRenderer eggBubble;
    [SerializeField]
    SpriteRenderer milkBubble;
    [SerializeField]
    SpriteRenderer flourBubble;

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

        eggBubble.enabled = false;
        milkBubble.enabled = false;
        flourBubble.enabled = false;
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

            eggBubble.enabled = false;
            milkBubble.enabled = false;
            flourBubble.enabled = false;

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

            if (ingredient.tag == "Egg") eggBubble.enabled = true;
            if (ingredient.tag == "Milk") milkBubble.enabled = true;
            if (ingredient.tag == "Flour") flourBubble.enabled = true;

            Destroy(ingredient.gameObject);
        }
    }
}
