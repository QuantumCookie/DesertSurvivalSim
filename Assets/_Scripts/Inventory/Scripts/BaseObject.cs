using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public enum ItemType
{
    Resource, Equipment, Consumable
}

public abstract class BaseObject : ScriptableObject
{
    public int id;
    public Sprite UIDisplay;
    [Space]
    [TextArea(15, 20)]
    public string description;
    public ItemType itemType;
    public int stackSize;
}
