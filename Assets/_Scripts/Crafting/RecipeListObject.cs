using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Crafting System/Recipe List", fileName = "New Recipe List")]
public class RecipeListObject : ScriptableObject
{
    public List<RecipeObject> list;
}
