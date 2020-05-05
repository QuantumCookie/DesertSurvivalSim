using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Collectable Resource/New Resource")]
public class ResourceSO : ScriptableObject
{
    public ResourceType type;
    public float damageNeeded;
    public BaseObject yield;
    public int quantity;
}
