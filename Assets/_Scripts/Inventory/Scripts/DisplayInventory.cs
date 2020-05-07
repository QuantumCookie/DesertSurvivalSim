using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayInventory : MonoBehaviour
{
    private Player_Inventory inventoryScript;
    private InventoryObject playerInventory;

    public GameObject displaySlotPrefab;
    
    private GameObject[] display;
    private Dictionary<GameObject, InventorySlot> map;

    private MouseItem mouseItem = new MouseItem();
    private GameObject lastActiveSlot;
    private bool slotOptionsActive = false;
    
    private void Start()
    {
        inventoryScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Inventory>();
        playerInventory = inventoryScript.inventory;
        
        CreateDisplay();
    }

    private void CreateDisplay()
    {
        display = new GameObject[playerInventory.maxSlots];
        map = new Dictionary<GameObject, InventorySlot>(playerInventory.maxSlots);

        for (int i = 0; i < playerInventory.maxSlots; i++)
        {
            GameObject go = Instantiate(displaySlotPrefab, Vector3.zero, Quaternion.identity, transform);

            if (playerInventory.items[i].amount != -1)
            {
                go.GetComponentInChildren<TextMeshProUGUI>().text = playerInventory.items[i].amount.ToString();
                go.GetComponentsInChildren<Image>()[1].sprite = playerInventory.items[i].item.displaySprite;
            }
            else
            {
                go.GetComponentInChildren<TextMeshProUGUI>().text = "";
                go.GetComponentsInChildren<Image>()[1].sprite = null;
            }

            AddEvent(go, EventTriggerType.PointerEnter, delegate { OnPointerEnter(go); });
            AddEvent(go, EventTriggerType.PointerExit, delegate { OnPointerExit(go); });
            AddEvent(go, EventTriggerType.PointerClick, delegate { OnPointerClick(go); });
            AddEvent(go, EventTriggerType.BeginDrag, delegate { OnBeginDrag(go); });
            AddEvent(go, EventTriggerType.Drag, delegate { OnDrag(go); });
            AddEvent(go, EventTriggerType.EndDrag, delegate { OnEndDrag(go); });

            Button[] options = go.GetComponentsInChildren<Button>();

            options[0].onClick.AddListener(() => Use(go));
            options[1].onClick.AddListener(() => Discard(go));
            
            /*for (int j = 0; j < options.Length; j++)
            {
                //Debug.Log(j);
                options[j].onClick.RemoveAllListeners();
                options[j].onClick.AddListener(() => SlotOptionClicked(go, j) );
            }*/
                
            go.transform.GetChild(2).gameObject.SetActive(false);
            display[i] = go;
            map[go] = playerInventory.items[i];
        }
    }

    private void Update()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        for (int i = 0; i < playerInventory.maxSlots; i++)
        {
            if (playerInventory.items[i].amount != -1)
            {
                display[i].GetComponentInChildren<TextMeshProUGUI>().text = playerInventory.items[i].amount.ToString();
                display[i].GetComponentsInChildren<Image>()[1].sprite = playerInventory.items[i].item.displaySprite;
            }
            else
            {
                display[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
                display[i].GetComponentsInChildren<Image>()[1].sprite = null;
                display[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    private void AddEvent(GameObject go, EventTriggerType eventTriggerType, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = go.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }

    private void OnPointerEnter(GameObject go)
    {
        //Debug.Log("Pointer entered inventory slot " + map[go].slot);
        if (mouseItem.gameObject)
        {
            mouseItem.hoverSlot = map[go];
        }
    }
    
    private void OnPointerExit(GameObject go)
    {
        if (mouseItem.gameObject)
        {
            mouseItem.hoverSlot = null;
        }
    }
    
    private void OnBeginDrag(GameObject go)
    {
        if(!slotOptionsActive && map[go].amount != -1)
        {
            mouseItem.origin = map[go];
            mouseItem.gameObject = new GameObject();
            
            mouseItem.gameObject.AddComponent<RectTransform>().sizeDelta = go.GetComponent<RectTransform>().sizeDelta;
            
            Image img = mouseItem.gameObject.AddComponent<Image>();
            img.sprite = map[go].item.displaySprite;
            img.raycastTarget = false;
            img.color = new Color(img.color.a, img.color.g, img.color.b, 0.7f);
            
            mouseItem.gameObject.transform.SetParent(transform.parent);
        }
    }
    
    private void OnDrag(GameObject go)
    {
        if (!slotOptionsActive && mouseItem.gameObject)
        {
            mouseItem.gameObject.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
    
    private void OnEndDrag(GameObject go)
    {
        if (slotOptionsActive || mouseItem.origin == null) return;
        
        if (mouseItem.hoverSlot == null)
        {
            inventoryScript.ClearSlot(mouseItem.origin.slot);
        }
        else
        {
            inventoryScript.SwapSlots(mouseItem.origin.slot, mouseItem.hoverSlot.slot);
        }
        
        Destroy(mouseItem.gameObject);
    }
    
    private void OnPointerClick(GameObject go)
    {
        if (slotOptionsActive)
        {
            lastActiveSlot.transform.GetChild(2).gameObject.SetActive(false);
            
            slotOptionsActive = false;
            return;
        }
        
        if (map[go].amount != -1)
        {
            slotOptionsActive = true;
            go.transform.GetChild(2).gameObject.SetActive(true);
            lastActiveSlot = go;
        }
    }
    
    //Slot Options

    private void Use(GameObject go)
    {
        //Debug.Log("Action at " + map[go].slot);
        inventoryScript.UseItem(map[go].slot);
        OnPointerClick(go);
    }

    private void Discard(GameObject go)
    {
        //Debug.Log("Discard " + map[go].slot);
        inventoryScript.DiscardItem(map[go].slot);
        OnPointerClick(go);
    }
    
    /*private void SlotOptionClicked(GameObject go, int option)
    {
        Debug.Log("clicked " + option);
        return;
        switch (option)
        {
            case 0: Debug.Log("First option clicked");
                break;
            
            case 1: inventoryScript.ClearSlot(map[go].slot);
                break;
        }
    }*/
}

public class MouseItem
{
    public GameObject gameObject;
    public InventorySlot origin;
    public InventorySlot hoverSlot;
}
