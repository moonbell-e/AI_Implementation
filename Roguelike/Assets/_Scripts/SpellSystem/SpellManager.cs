using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    [SerializeField] private PlayerInputReader _playerInputReader;
    [SerializeField] private InventoryController _inventoryController;
    
    public GameObject _inventoryContainer;

    public Action<int> OnItemAdded;
    public Action<int> OnItemRemoved;
    public Action<int> OnFirstItemAdded;
    public Action<int> OnFirstItemRemoved;
    public Action<int> OnFull;

    public Action<int, int, int> OnSpellCasted;

    List<Item> usedItems;
    List<int> usedItemsInventoryId;

    protected PlayerInputReader PlayerInputReader => _playerInputReader;
    protected InventoryController InventoryController => _inventoryController;

    private void Start()
    {
        usedItems = new List<Item>();
        usedItemsInventoryId = new List<int>();
    }

    private void UseItem(Item item, int id)
    {
        for (int i = 0; i < usedItems.Count; i++) 
        { 
            if (usedItems[i]._id == item._id) 
            {
                OnItemRemoved.Invoke(id);
                if (usedItems[0]._id == item._id)
                {
                    OnFirstItemRemoved.Invoke(id);
                    if (usedItemsInventoryId.Count > 1)
                    {
                        OnFirstItemAdded.Invoke(usedItemsInventoryId[1]);
                    }
                }

                usedItems.Remove(item);
                usedItemsInventoryId.Remove(id);
                return;
            }
        }
        if (usedItems.Count < 3)
        {
            usedItems.Add(item);
            usedItemsInventoryId.Add(id);

            OnItemAdded.Invoke(id);
            if (usedItems.Count == 1)
            {
                OnFirstItemAdded.Invoke(id);
            }
        }
        else
        {
            OnFull.Invoke(id);
        }
    }

    private void SpellCast()
    {
        if (usedItems.Count == 3)
        {
            OnSpellCasted.Invoke(usedItems[0]._id - 1, usedItems[1]._id - 1, usedItems[2]._id - 1);
            usedItems.RemoveAt(0);
            usedItems.RemoveAt(0);
            usedItems.RemoveAt(0);
            usedItemsInventoryId.Remove(0);
            usedItemsInventoryId.Remove(0);
            usedItemsInventoryId.Remove(0);
        }
    }

    private void OnEnable()
    {
        InventoryController.OnItemUsed += UseItem;
        PlayerInputReader.OnSpellTriggered += SpellCast;
    }

    private void OnDisable()
    {
        InventoryController.OnItemUsed -= UseItem;
        PlayerInputReader.OnSpellTriggered -= SpellCast;
    }
}
