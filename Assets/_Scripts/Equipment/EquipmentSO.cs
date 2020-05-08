﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Equipment/New Equipment")]
public class EquipmentSO : ScriptableObject
{
    public int id;
    public GameObject prefab;
    public float damage;
    public ResourceType[] harvests;
}