using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipes : Persistent<Recipes>
{
    public Recipe friedEgg;

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