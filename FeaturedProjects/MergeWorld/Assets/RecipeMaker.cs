using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeMaker : MonoBehaviour
{

    public List<Entity> listOfAllEntitys;

    public Dictionary<string, Entity> a = new Dictionary<string, Entity>();

    public List<Recipe> recipes = new List<Recipe>(0);

    private void Start()
    {
        foreach (Entity entity in listOfAllEntitys)
        {
            a.Add(entity._name, entity);
        }

        AddRecipes();
    }

    public Recipe FindRecipe(string a, string b)
    {
        a = a.ToLower();
        b = b.ToLower();

        foreach (Recipe recipe in recipes)
        {
            if (recipe.ingredientA == a)
            {
                if(recipe.ingredientB == b)
                {
                    return recipe;
                }
            }
            else if (recipe.ingredientB == a)
            {
                if(recipe.ingredientA == b)
                {
                    return recipe;
                }
            }
        }

        return null;
    }

    private void AddRecipes()
    {
        recipes.Add(new Recipe("Stone", "Log", true, true, new string[] { "Camp Fire" }));
        recipes.Add(new Recipe("Chicken Meat", "Camp Fire", true, false, new string[] { "Steaked Chicken" }));
        recipes.Add(new Recipe("Stone", "Camp Fire", true, false, new string[] { "Iron" }));
        // recipes.Add(new Recipe("Sapling", "Iron", true, false, new string[] { "Rope" }));
        recipes.Add(new Recipe("Chicken Meat", "rope", true, true, new string[] { "Bag" }));
        recipes.Add(new Recipe("Log", "Iron", true, true, new string[] { "Axe" }));
        recipes.Add(new Recipe("Log", "Axe", true, false, new string[] { "Planks" }));
        // recipes.Add(new Recipe("Tree", "Axe", true, false, new string[] { "Log", "Log" }));
        recipes.Add(new Recipe("Rope", "Rope", true, true, new string[] { "Net" }));
        recipes.Add(new Recipe("Log", "Rope", true, true, new string[] { "Fishing Rod" }));
        recipes.Add(new Recipe("Chicken", "Axe", true, false, new string[] { "Chicken Meat" }));
        recipes.Add(new Recipe("Sapling", "Axe", true, false, new string[] { "Stone" }));
        recipes.Add(new Recipe("Fishing Rod", "Net", true, true, new string[] { "Trap" }));
        recipes.Add(new Recipe("Iron", "Iron", true, true, new string[] { "Sissors" }));
        recipes.Add(new Recipe("Sheep", "Sissors", false, false, new string[] { "Wool" /*, "Cut Sheep"*/ }));
        recipes.Add(new Recipe("Stone", "Planks", true, true, new string[] { "Wheel" }));
        recipes.Add(new Recipe("Wheel", "Log", true, true, new string[] { "Spinning Wheel" }));
        recipes.Add(new Recipe("Spinning Wheel", "Wool", false, true, new string[] { "Yarn" }));
        recipes.Add(new Recipe("Yarn", "Yarn", true, true, new string[] { "Rope" }));

        recipes.Add(new Recipe("Planks", "Iron", true, true, new string[] { "Sword" }));
        recipes.Add(new Recipe("Wheel", "Wheel", true, true, new string[] { "Bike" }));
        recipes.Add(new Recipe("Bike", "Bike", true, true, new string[] { "Car" }));
        recipes.Add(new Recipe("Fishing Rod", "Lake", false, false, new string[] { "Fish" }));
        recipes.Add(new Recipe("Planks", "Planks", true, true, new string[] { "Hut" }));




        // recipes.Add(new Recipe("Human", "Human", false, false, new string[] { "Human" }));

        recipes.Add(new Recipe("Human", "Camp Fire", true, false, new string[] { "Steaked Chicken" }));
        recipes.Add(new Recipe("Baby", "Camp Fire", true, false, new string[] { "Chicken Meat" }));


        // Add following: 

        // recipes.Add(new Recipe("Planks", "Planks", true, true, new string[] { "Something" }));

    }
}

public class Recipe
{
    public string ingredientA;
    public string ingredientB;
    public bool destroyA;
    public bool destroyB;
    public Entity[] result;

    public Recipe(string ingredientA, string ingredientB, bool destroyA, bool destroyB, string[] result)
    {
        this.ingredientA = ingredientA.ToLower();
        this.ingredientB = ingredientB.ToLower();
        this.destroyA = destroyA;
        this.destroyB = destroyB;

        Entity[] entitys = new Entity[result.Length];
        for (int i = 0; i < result.Length; i++)
        {
            Debug.Log(ingredientA + ingredientB);
            entitys[i] = GameManager.instance.recipeMaker.a[result[i]];
        }
        this.result = entitys;
    }

}