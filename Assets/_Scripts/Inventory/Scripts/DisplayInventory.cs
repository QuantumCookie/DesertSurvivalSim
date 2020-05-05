using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject playerInventory;

    private Dictionary<InventorySlot, GameObject> itemsDisplayed;

    private void Start()
    {
        CreateDisplay();
    }

    private void CreateDisplay()
    {
        itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
        
        for (int i = 0; i < playerInventory.items.Count; i++)
        {
            GameObject g = Instantiate(playerInventory.items[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            itemsDisplayed.Add(playerInventory.items[i], g);
        }
    }

    private void Update()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        for (int i = 0; i < playerInventory.items.Count; i++)
        {
            if (!itemsDisplayed.ContainsKey(playerInventory.items[i]))
            {
                GameObject g = Instantiate(playerInventory.items[i].item.prefab, Vector3.zero, Quaternion.identity, transform);   
                itemsDisplayed.Add(playerInventory.items[i], g);
            }
            else
            {
                itemsDisplayed[playerInventory.items[i]].GetComponentInChildren<TextMeshProUGUI>().text =
                    playerInventory.items[i].amount.ToString("n0");
            }
            
            itemsDisplayed[playerInventory.items[i]].GetComponentsInChildren<Image>()[1].sprite =
                playerInventory.items[i].item.displaySprite;
        }
    }
}
