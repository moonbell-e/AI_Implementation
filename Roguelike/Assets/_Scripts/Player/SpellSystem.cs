using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellSystem : MonoBehaviour
{
    [SerializeField] private PlayerInputReader _playerInputReader;
    [SerializeField] private InventoryController _inventoryController;
    [SerializeField] private TMP_Text _text;

    public GameObject _inventoryContainer;

    List<Item> usedItems;

    protected PlayerInputReader PlayerInputReader => _playerInputReader;
    protected InventoryController InventoryController => _inventoryController;

    private void Start()
    {
        usedItems = new List<Item>();
    }

    private void AddItem(Item item)
    {
        bool remove = false;
        for (int i = 0; i < usedItems.Count; i++) 
        { 
            if (usedItems[i]._id == item._id) 
            {
                remove = true;
            }
        }
        if (remove)
        {
            _text.text = ("removed " + item._description);
            usedItems.Remove(item);
        }
        else
        {
            if (usedItems.Count < 3)
            {
                _text.text = ("added " + item._description);
                usedItems.Add(item);
            }
            else
            {
                _text.text = ("full");
            }
        }
    }

    private void SpellCast()
    {
        if (usedItems.Count == 3)
        {
            _text.text = (usedItems[0]._activeSkillDescription + " " + usedItems[1]._passiveSkillDescription + " " + usedItems[2]._passiveSkillDescription);
            usedItems.RemoveAt(0);
            usedItems.RemoveAt(0);
            usedItems.RemoveAt(0);
        }
    }

    private void OnEnable()
    {
        InventoryController.OnItemUsed += AddItem;
        PlayerInputReader.OnSpellTriggered += SpellCast;
    }

    private void OnDisable()
    {
        InventoryController.OnItemUsed -= AddItem;
        PlayerInputReader.OnSpellTriggered -= SpellCast;
    }
}
