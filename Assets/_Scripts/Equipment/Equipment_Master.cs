using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_Master : MonoBehaviour
{
    [SerializeField] private EquipmentSO _equipment;
    public EquipmentSO equipment => _equipment;
}
