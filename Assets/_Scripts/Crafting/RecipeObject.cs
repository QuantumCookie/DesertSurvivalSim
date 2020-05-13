using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Crafting System/Recipe", fileName = "New Recipe")]
public class RecipeObject : ScriptableObject
{
    public RecipeItem[] ingredients;
    public float craftTime;
    public RecipeItem product;
}

[System.Serializable]
public class RecipeItem
{
    public bool isBuilding;
    public BaseObject item;
    public GameObject buildingPrefab;
    public int quantity;
}
