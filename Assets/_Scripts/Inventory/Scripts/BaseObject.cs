﻿using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public enum ItemType
{
    Resource, Equipment, Consumable, Null
}

public enum ItemAttributeType
{
    Hydration, Hunger, Damage
}

[System.Serializable]
public class ItemAttribute
{
    public ItemAttributeType type;
    public float value;
}

public abstract class BaseObject : ScriptableObject
{
    public int id;
    public Sprite displaySprite;
    
    [Space]
    [TextArea(15, 20)]
    [Space]
    
    public string description;
    public ItemType itemType;
    
    [Space]
    
    public int stackSize;

    public ItemAttribute[] attributes;
}
