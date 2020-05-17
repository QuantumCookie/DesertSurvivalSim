using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_UseItem : MonoBehaviour
{
    private Player_Hydration playerHydration;

    private void Start()
    {
        playerHydration = GetComponent<Player_Hydration>();
    }

    public void UseItem(BaseObject item)
    {
        if (item.itemType == ItemType.Consumable)
        {
            ItemAttribute[] attributes = item.attributes;

            foreach (ItemAttribute i in attributes)
            {
                if (i.type == ItemAttributeType.Hydration)
                {
                    playerHydration.Replenish(i.value);
                }
                else if (i.type == ItemAttributeType.Damage)
                {
                    playerHydration.GetComponent<Player_Health>().Add(i.value);
                }
            }
        }
    }
}
