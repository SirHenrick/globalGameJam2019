using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipes : Persistent<Recipes>
{
    List<Recipe> easy;
    List<Recipe> medium;
    List<Recipe> hard;

    float overallTime = 0f;
    float timeSinceRequest = 0f;

    // Attributes
    const float minute = 60f;
    const float easyToMediumTime = minute * 4.5f;
    const float mediumToHard = minute * 4f;
    const float easyTimeInterval = 5f;
    const float mediumTimeInterval = 55f;
    const float hardTimeInterval = 50f;

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
            iceCream,
            noodle
        };

        medium = new List<Recipe>
        {
            friedEgg,
            iceCream,
            noodle,
            pudding,
            pancakes
        };

        hard = new List<Recipe>
        {
            noodle,
            pudding,
            pancakes,
            cake
        };
    }

    void Update()
    {
        if (overallTime < easyToMediumTime)
        {
            CreateRequest(easyTimeInterval, easy);
        }
        else if (overallTime < mediumToHard)
        {
            CreateRequest(mediumTimeInterval, medium);
        }
        else
        {
            CreateRequest(hardTimeInterval, hard);
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
                    case "Pancake":
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
                        Debug.Log(recipe.result.name);
                    }
                    else recipeRequest.SetActive(false);
                }
                   
            }
            else order.SetActive(false);
        }

        overallTime += Time.deltaTime;
        timeSinceRequest += Time.deltaTime;
    }

    void CreateRequest(float timeInterval, List<Recipe> recipes)
    {
        if (timeSinceRequest >= timeInterval)
        {
            timeSinceRequest = 0f;

            if (requests.Count < 7)
            {
                var randomIndex = UnityEngine.Random.Range(0, recipes.Count);
                requests.Add(recipes[randomIndex]);
            }
        }
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
