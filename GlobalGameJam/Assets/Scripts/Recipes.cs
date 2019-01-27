using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Recipes : Persistent<Recipes>
{
    List<Recipe> easy;
    List<Recipe> medium;
    List<Recipe> hard;
    List<Recipe> impossible;

    float overallTime = 0f;
    float timeSinceRequest = 0f;
    bool doggoMedium = false;
    bool doggoHard = false;
    bool doggoImpossible = false;
    int ordersCompleted = 0;

    // Attributes
    const float minute = 60f;
    const float easyToMediumTime = minute * 1f;
    const float mediumToHard = minute * 3f;
    const float hardToImpossible = minute * 5f;
    const float easyTimeInterval = 10f;
    const float mediumTimeInterval = 9f;
    const float hardTimeInterval = 8f;
    const float impossibleTimeInterval = 6f;

    public List<Recipe> requests;

    public Recipe friedEgg;
    public Recipe iceCream;
    public Recipe noodle;
    public Recipe pudding;
    public Recipe pancakes;
    public Recipe frenchToast;
    public Recipe cake;
    public Recipe garbage;
    public Transform canvas;
    public Vector2 doggoSpawnPoint;
    public GameObject doggo;

    void Start()
    {
        requests = new List<Recipe>()
        {
            friedEgg
        };

        easy = new List<Recipe>
        {
            friedEgg,
            friedEgg,
            friedEgg,
            iceCream,
            noodle
        };

        medium = new List<Recipe>
        {
            friedEgg,
            iceCream,
            noodle,
            pudding,
            pancakes,
            frenchToast
        };

        hard = new List<Recipe>
        {
            friedEgg,
            iceCream,
            noodle,
            pudding,
            pudding,
            pudding,
            pancakes,
            pancakes,
            pancakes,
            frenchToast,
            frenchToast,
            frenchToast,
            cake,
            cake,
            cake,
        };

        impossible = new List<Recipe>
        {
            pudding,
            pancakes,
            frenchToast,
            cake,
            cake,
            cake,
        };
    }

    void Update()
    {
        if (overallTime < easyToMediumTime)
        {
            CreateRequest(easyTimeInterval, easy, "easy");
        }
        else if (overallTime < mediumToHard)
        {
            if (!doggoMedium)
            {
                var dog = Instantiate(doggo);
                dog.transform.position = doggoSpawnPoint;
                doggoMedium = true;
            }

            CreateRequest(mediumTimeInterval, medium, "medium");
        }
        else if (overallTime < hardToImpossible)
        {
            if (!doggoHard)
            {
                var dog = Instantiate(doggo);
                dog.transform.position = doggoSpawnPoint;
                doggoHard = true;
            }

            CreateRequest(hardTimeInterval, hard, "hard");
        }
        else
        {
            if (!doggoImpossible)
            {
                var dog = Instantiate(doggo);
                dog.transform.position = doggoSpawnPoint;
                doggoImpossible = true;
            }

            CreateRequest(impossibleTimeInterval, impossible, "impossible");
        }

        for (var i = 0; i < canvas.childCount; i++)
        {
            var order = canvas.GetChild(i).gameObject;

            if (i < requests.Count)
            {
                order.SetActive(true);
                var recipe = requests[i];
                var index = 0;
                switch (recipe.result.name)
                {
                    case "Fried Egg":
                        index = 0;
                        break;
                    case "Ice Cream":
                        index = 1;
                        break;
                    case "Noodle":
                        index = 2;
                        break;
                    case "Pudding":
                        index = 3;
                        break;
                    case "Pancakes":
                        index = 4;
                        break;
                    case "French Toast":
                        index = 5;
                        break;
                    case "Cake":
                        index = 6;
                        break;
                }

                for (var j = 0; j < order.transform.childCount; j++)
                {
                    var recipeRequest = order.transform.GetChild(j).gameObject;
                    if (j == index)
                    {
                        recipeRequest.SetActive(true);
                    }
                    else recipeRequest.SetActive(false);
                }
                   
            }
            else order.SetActive(false);
        }

        overallTime += Time.deltaTime;
        timeSinceRequest += Time.deltaTime;
    }

    void CreateRequest(float timeInterval, List<Recipe> recipes, string difficulty)
    {
        if (timeSinceRequest >= timeInterval)
        {
            timeSinceRequest = 0f;

            if (requests.Count < 7)
            {
                var randomIndex = UnityEngine.Random.Range(0, recipes.Count);
                Debug.Log(difficulty);
                requests.Add(recipes[randomIndex]);
            }
            else
            {
                SceneManager.LoadScene("Game Over");
            }
        }
    }

    public bool FulfillOrder(string name)
    {
        var newRequests = new List<Recipe>();
        var fulfilled = false;
        foreach(var recipe in requests)
        {
            if (fulfilled || recipe.result.name != name)
                newRequests.Add(recipe);
            else fulfilled = true;
        }

        requests = newRequests;

        return fulfilled;
    }
}

[Serializable]
public class Recipe
{
    public List<Ingredient> ingredients;
    public GameObject result;
}

public enum RecipesIndex
{
    friedEgg = 0,
    iceCream = 1,
    noodle = 2,
    pudding = 3,
    pancake = 4,
    frenchToast = 5,
    cake = 6
}
