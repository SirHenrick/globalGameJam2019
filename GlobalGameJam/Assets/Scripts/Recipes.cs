using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipes : Persistent<Recipes>
{
    public Recipe friedEgg;
    public Recipe iceCream;
    public Recipe noodle;
    public Recipe pudding;
    public Recipe pancakes;
    public Recipe frenchToast;
    public Recipe cake;
    public Recipe garbage;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

[Serializable]
public class Recipe
{
    public List<Ingredient> ingredients;
    public GameObject result;
}