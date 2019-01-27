using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : MonoBehaviour
{
    List<string> cookingIngredients;
    List<Recipe> recipes;
    Recipe garbageRecipe;
    Sprite originalSprite;
    SpriteRenderer spriteRenderer;
    float cookTimer = 0f;
    int progressbarFrame = 0;
    float cookingProgress = 0;
    float brokenTimer = 0;
    bool isBroken = false;

    public SpriteRenderer eggBubble;
    public SpriteRenderer milkBubble;
    public SpriteRenderer flourBubble;
    public SpriteRenderer progressBar;

    public List<Sprite> progressBarSprites = new List<Sprite>();

    public AudioSource audioPlayer;

    // Attributes
    public float cookDuration = 2f;
    public float creationSpeed = .75f;
    public float startOffset = 1;
    public Sprite on;
    public Sprite brokenSprite;
    public float brokenCooldown = 5f;

    public AudioClip addItemSound;
    public AudioClip doneSound;
    public AudioClip workingSound;
    public AudioClip garbage;

    void Start()
    {
        cookingIngredients = new List<string>();
        recipes = new List<Recipe>()
        {

            Recipes.instance.friedEgg,
            Recipes.instance.noodle,
            Recipes.instance.pancakes,
            Recipes.instance.frenchToast,
            Recipes.instance.cake,
            Recipes.instance.iceCream,
            Recipes.instance.pudding
        };

        garbageRecipe = Recipes.instance.garbage;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;

        eggBubble.enabled = false;
        milkBubble.enabled = false;
        flourBubble.enabled = false;
        progressBar.enabled = false;
    }

    void Update()
    {
        if (!isBroken)
        {
            if (cookingIngredients.Count > 0)
                spriteRenderer.sprite = on;
            else
                spriteRenderer.sprite = originalSprite;
        }else
        {
            spriteRenderer.sprite = brokenSprite;
        }

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

                        }
                    }

                    if (equal)
                    {
                        finalRecipe = recipe;
                        break;
                    }
                }

                if (equal)
                {
                    var dish = Instantiate(finalRecipe.result);
                    dish.transform.position = new Vector2(transform.position.x, transform.position.y - startOffset);
                    dish.GetComponent<Rigidbody2D>().AddForce(Vector2.down * creationSpeed, ForceMode2D.Impulse);

                    audioPlayer.PlayOneShot(doneSound);
                }
                else
                {
                    var dish = Instantiate(garbageRecipe.result);
                    dish.transform.position = new Vector2(transform.position.x, transform.position.y - startOffset);
                    dish.GetComponent<Rigidbody2D>().AddForce(Vector2.down * creationSpeed, ForceMode2D.Impulse);

                    audioPlayer.PlayOneShot(garbage);
                }

                cookingIngredients = new List<string>();

                eggBubble.enabled = false;
                milkBubble.enabled = false;
                flourBubble.enabled = false;
                progressBar.enabled = false;

                brokenTimer = brokenCooldown;
                isBroken = true;
            }


        cookingProgress = (1 - (cookTimer / cookDuration)) * 100;

        cookTimer -= Time.deltaTime;

        if (cookingProgress < 6.67)
        {
            progressbarFrame = 0;
            progressBar.sprite = progressBarSprites[progressbarFrame];
        }
        else if (cookingProgress < 6.67 * 2)
        {
            progressbarFrame = 1;
            progressBar.sprite = progressBarSprites[progressbarFrame];
        }
        else if (cookingProgress < 6.67 * 3)
        {
            progressbarFrame = 2;
            progressBar.sprite = progressBarSprites[progressbarFrame];
        }
        else if (cookingProgress < 6.67 * 4)
        {
            progressbarFrame = 3;
            progressBar.sprite = progressBarSprites[progressbarFrame];
        }
        else if (cookingProgress < 6.67 * 5)
        {
            progressbarFrame = 4;
            progressBar.sprite = progressBarSprites[progressbarFrame];
        }
        else if (cookingProgress < 6.67 * 6)
        {
            progressbarFrame = 5;
            progressBar.sprite = progressBarSprites[progressbarFrame];
        }
        else if (cookingProgress < 6.67 * 7)
        {
            progressbarFrame = 6;
            progressBar.sprite = progressBarSprites[progressbarFrame];
        }
        else if (cookingProgress < 6.67 * 8)
        {
            progressbarFrame = 7;
            progressBar.sprite = progressBarSprites[progressbarFrame];
        }
        else if (cookingProgress < 6.67 * 9)
        {
            progressbarFrame = 8;
            progressBar.sprite = progressBarSprites[progressbarFrame];
        }
        else if (cookingProgress < 6.67 * 10)
        {
            progressbarFrame = 9;
            progressBar.sprite = progressBarSprites[progressbarFrame];
        }
        else if (cookingProgress < 6.67 * 11)
        {
            progressbarFrame = 10;
            progressBar.sprite = progressBarSprites[progressbarFrame];
        }
        else if (cookingProgress < 6.67 * 12)
        {
            progressbarFrame = 11;
            progressBar.sprite = progressBarSprites[progressbarFrame];
        }
        else if (cookingProgress < 6.67 * 13)
        {
            progressbarFrame = 12;
            progressBar.sprite = progressBarSprites[progressbarFrame];
        }
        else if (cookingProgress < 6.67 * 14)
        {
            progressbarFrame = 13;
            progressBar.sprite = progressBarSprites[progressbarFrame];
        }
        else if (cookingProgress < 6.67 * 15)
        {
            progressbarFrame = 14;
            progressBar.sprite = progressBarSprites[progressbarFrame];
        }

        if (isBroken)
        {
            brokenTimer -= Time.deltaTime;
            if (brokenTimer <= 0)
            {
                isBroken = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isBroken)
        {
            var ingredient = collision.gameObject.GetComponent<Ingredient>();
            if (ingredient != null)
            {
                if (!cookingIngredients.Contains(ingredient.tag))
                {
                    audioPlayer.PlayOneShot(addItemSound);

                    cookingIngredients.Add(ingredient.tag);
                    cookTimer = cookDuration;

                    if (ingredient.tag == "Egg") eggBubble.enabled = true;
                    if (ingredient.tag == "Milk") milkBubble.enabled = true;
                    if (ingredient.tag == "Flour") flourBubble.enabled = true;

                    progressbarFrame = 0;
                    progressBar.sprite = progressBarSprites[progressbarFrame];
                    progressBar.enabled = true;

                    Destroy(ingredient.gameObject);

                    audioPlayer.PlayOneShot(workingSound);
                }
            }
        }
    }
}
