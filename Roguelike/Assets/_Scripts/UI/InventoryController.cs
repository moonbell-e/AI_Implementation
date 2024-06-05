using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ItemCollect _itemCollect;
    [SerializeField] private SpellManager _spellSystem;

    public List<Item> items;

    public GameObject _inventoryContainer;
    public Animator _inventoryAnimator;
    public Animator _inventoryButtonNamesAnimator;

    protected InputReaderSwitcher InputReaderSwitcher => _player.InputReaderSwitcher;
    protected PlayerInputReader PlayerInputReader => _player.PlayerInputReader;
    protected CookingInputReader CookingInputReader => _player.CookingInputReader;
    protected InventoryInputReader InventoryInputReader => _player.InventoryInputReader;
    protected ItemCollect ItemCollect => _itemCollect;
    protected SpellManager SpellSystem => _spellSystem;

    public Action<Item, int> OnItemUsed;

    private void Start()
    {
        items = new List<Item>();
        for (int i = 0; i < _inventoryContainer.transform.childCount; i++) 
        {
            items.Add(gameObject.AddComponent<Item>());
        }
    }
    
    private void Update()
    {
        CookingMode();
    }

    private void InventoryOpen()
    {
        if (_inventoryAnimator.GetCurrentAnimatorStateInfo(0).IsName("InventoryClosed"))
        {
            _inventoryAnimator.SetTrigger("InventoryOpen");
            InputReaderSwitcher.SetActiveInputReader(InventoryInputReader);
        }
        else
        {
            if (_inventoryAnimator.GetCurrentAnimatorStateInfo(0).IsName("InventoryOpened"))
            {
                _inventoryAnimator.SetTrigger("InventoryClose");
                InputReaderSwitcher.SetActiveInputReader(PlayerInputReader);
            }
        }
    }

    private void CookingMode()
    {
        if (PlayerInputReader.IsCooking)
        {
            if (_inventoryAnimator.GetCurrentAnimatorStateInfo(0).IsName("InventoryClosed"))
            {
                _inventoryAnimator.SetTrigger("CookingOpen");
                _inventoryButtonNamesAnimator.SetTrigger("ButtonNamesOpen");

                InputReaderSwitcher.CookingModeEnable();
            }
        }
        else
        {
            if (_inventoryAnimator.GetCurrentAnimatorStateInfo(0).IsName("CookingOpened"))
            {
                _inventoryAnimator.SetTrigger("CookingClose");
                _inventoryButtonNamesAnimator.SetTrigger("ButtonNamesClose");
                InputReaderSwitcher.CookingModeDisable();
            }
        }
    }

    private void ItemCollected(Item item)
    {
        if (item._isStackable)
        {
            AddStackableItem(item);
        }
        else
        {
            AddUnstackableItem(item);
        }
    }

    private void AddStackableItem(Item item)
    {
        for (int i = 0; i < items.Count; ++i)
        {
            if (items[i]._id == item._id)
            {
                items[i]._count += 1;
                DisplayCount(i);
                return;
            }
        }
        for (int i = 0; i < items.Count; ++i)
        {
            if (items[i]._id == 0)
            {
                items[i] = item;
                items[i]._count += 1;
                DisplayItem(i);
                DisplayCount(i);
                break;
            }
        }
    }

    private void AddUnstackableItem(Item item)
    {
        for (int i = 0; i < items.Count; ++i)
        {
            if (items[i]._id == 0)
            {
                items[i] = item;
                DisplayItem(i);
                break;
            }
        }
    }

    private void DisplayItem(int i)
    {
        Transform cell = _inventoryContainer.transform.GetChild(i);
        Transform icon = cell.GetChild(1);
        Image image = icon.GetComponent<Image>();
        image.enabled = true;
        image.sprite = Resources.Load<Sprite>(items[i]._pathInventoryIcon);
    }

    private void DisplayCount(int i)
    {
        Transform cell = _inventoryContainer.transform.GetChild(i);
        Transform text = cell.GetChild(5);
        TMP_Text count = text.GetComponent<TMP_Text>();
        count.enabled = true;
        count.text = (items[i]._count.ToString());
    }

    private void ItemUsed(int id)
    {
        if (items[id]._id != 0)
        {
            OnItemUsed.Invoke(items[id], id);
        }
    }

    private void ItemAdd(int id)
    {
        Transform cell = _inventoryContainer.transform.GetChild(id);
        Transform icon = cell.GetChild(2);
        Image image = icon.GetComponent<Image>();
        image.enabled = true;
    }

    private void ItemRemove(int id)
    {
        Transform cell = _inventoryContainer.transform.GetChild(id);
        Transform icon = cell.GetChild(2);
        Image image = icon.GetComponent<Image>();
        image.enabled = false;
    }

    private void FirstItemAdd(int id)
    {
        Transform cell = _inventoryContainer.transform.GetChild(id);
        Transform icon = cell.GetChild(4);
        Image image = icon.GetComponent<Image>();
        image.enabled = true;
    }

    private void FirstItemRemove(int id)
    {
        Transform cell = _inventoryContainer.transform.GetChild(id);
        Transform icon = cell.GetChild(4);
        Image image = icon.GetComponent<Image>();
        image.enabled = false;
    }

    private void ItemFull(int id)
    {
        Transform cell = _inventoryContainer.transform.GetChild(id);
        Transform icon = cell.GetChild(3);
        Image image = icon.GetComponent<Image>();
        StartCoroutine(ItemDidntUseCoroutine(image));
    }

    IEnumerator ItemDidntUseCoroutine(Image image)
    {
        image.enabled = true;
        yield return new WaitForSeconds(0.2f);
        image.enabled = false;
    }

    private void SpellUsed()
    {
        for (int i = 0; i < items.Count; ++i)
        {
            Transform cell = _inventoryContainer.transform.GetChild(i);
            Transform icon = cell.GetChild(2);
            Image image = icon.GetComponent<Image>();
            image.enabled = false;
            icon = cell.GetChild(4);
            image = icon.GetComponent<Image>();
            image.enabled = false;
        }
    }

    private void OnEnable()
    {
        PlayerInputReader.OnInventoryTriggered += InventoryOpen;
        PlayerInputReader.OnSpellTriggered += SpellUsed;

        CookingInputReader.OnIngredient1Triggered += ItemUsed;
        CookingInputReader.OnIngredient2Triggered += ItemUsed;
        CookingInputReader.OnIngredient3Triggered += ItemUsed;
        CookingInputReader.OnIngredient4Triggered += ItemUsed;
        CookingInputReader.OnIngredient5Triggered += ItemUsed;
        CookingInputReader.OnIngredient6Triggered += ItemUsed;

        InventoryInputReader.OnCloseInventoryTriggered += InventoryOpen;

        ItemCollect.OnItemCollected += ItemCollected;

        SpellSystem.OnItemAdded += ItemAdd;
        SpellSystem.OnItemRemoved += ItemRemove;
        SpellSystem.OnFirstItemAdded += FirstItemAdd;
        SpellSystem.OnFirstItemRemoved += FirstItemRemove;
        SpellSystem.OnFull += ItemFull;
    }

    private void OnDisable()
    {
        PlayerInputReader.OnInventoryTriggered -= InventoryOpen;
        PlayerInputReader.OnSpellTriggered -= SpellUsed;

        CookingInputReader.OnIngredient1Triggered -= ItemUsed;
        CookingInputReader.OnIngredient2Triggered -= ItemUsed;
        CookingInputReader.OnIngredient3Triggered -= ItemUsed;
        CookingInputReader.OnIngredient4Triggered -= ItemUsed;
        CookingInputReader.OnIngredient5Triggered -= ItemUsed;
        CookingInputReader.OnIngredient6Triggered -= ItemUsed;

        InventoryInputReader.OnCloseInventoryTriggered -= InventoryOpen;

        ItemCollect.OnItemCollected -= ItemCollected;

        SpellSystem.OnItemAdded -= ItemAdd;
        SpellSystem.OnItemRemoved -= ItemRemove;
        SpellSystem.OnFirstItemAdded -= FirstItemAdd;
        SpellSystem.OnFirstItemRemoved -= FirstItemRemove;
        SpellSystem.OnFull -= ItemFull;
    }
}
