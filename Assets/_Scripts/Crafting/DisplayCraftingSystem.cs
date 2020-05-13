using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCraftingSystem : MonoBehaviour
{
    public RecipeListObject recipes;
    public GameObject uiContent;
    public GameObject craftableItemPrefab;

    private Player_Crafting playerCrafting;
    
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        playerCrafting = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Crafting>();
        CreateSlots();
    }

    private void CreateSlots()
    {
        for (int i = 0; i < recipes.list.Count; i++)
        {
            CraftableItem item = Instantiate(craftableItemPrefab, uiContent.transform).GetComponent<CraftableItem>();

            RecipeObject recipe = recipes.list[i];
            
            item.Set(recipe);
            item.GetButton().onClick.AddListener(delegate { playerCrafting.Craft(recipe); });
        }
    }

    /*private void Craft(RecipeObject recipe)
    {
        for (int i = 0; i < recipe.ingredients.Length; i++)
        {
            if(playerInventory.GetTotalItemCount(recipe.ingredients[i].item) < recipe.ingredients[i].quantity) return;
        }
        
        for (int i = 0; i < recipe.ingredients.Length; i++)
        {
            playerInventory.RemoveItem(recipe.ingredients[i].item, recipe.ingredients[i].quantity);
        }
        
        playerInventory.AddItem(recipe.product.item, recipe.product.quantity);
    }*/
}
