using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Crafting : MonoBehaviour
{
    private Player_Inventory playerInventory;
    
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Inventory>();
    }

    public void Craft(RecipeObject recipe)
    {
        //Debug.Log("Trying to craft");
        for (int i = 0; i < recipe.ingredients.Length; i++)
        {
            if(playerInventory.GetTotalItemCount(recipe.ingredients[i].item) < recipe.ingredients[i].quantity) return;
        }
        
        for (int i = 0; i < recipe.ingredients.Length; i++)
        {
            playerInventory.RemoveItem(recipe.ingredients[i].item, recipe.ingredients[i].quantity);
        }
        
        playerInventory.AddItem(recipe.product.item, recipe.product.quantity);
        Debug.Log("Crafted " + recipe.product.item.name);
    }
}
