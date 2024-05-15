using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ItemCollect _itemCollect;
    [SerializeField] private SpellSystem _spellSystem;

    List<Item> items;

    public GameObject _inventoryContainer;
    public Animator _inventoryAnimator;
    public Animator _inventoryButtonNamesAnimator;

    protected InputReaderSwitcher InputReaderSwitcher => _player.InputReaderSwitcher;
    protected PlayerInputReader PlayerInputReader => _player.PlayerInputReader;
    protected CookingInputReader CookingInputReader => _player.CookingInputReader;
    protected InventoryInputReader InventoryInputReader => _player.InventoryInputReader;
    protected ItemCollect ItemCollect => _itemCollect;
    protected SpellSystem SpellSystem => _spellSystem;

    public Action<Item> OnItemUsed;

    int _usedItemsCount = 0;

    private void Start()
    {
        items = new List<Item>();
        for (int i = 0; i < _inventoryContainer.transform.childCount; i++) 
        {
            // items.Add(new Item());
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

    private void ItemUsed(int id)
    {
        if (items[id]._id != 0)
        {
            OnItemUsed.Invoke(items[id]);

            Transform cell = _inventoryContainer.transform.GetChild(id);
            Transform icon = cell.GetChild(2);
            Image image = icon.GetComponent<Image>();

            if (image.enabled)
            {
                image.enabled = false;
                _usedItemsCount -= 1;
            }
            else
            {
                if (_usedItemsCount < 3)
                {
                    image.enabled = true;
                    if (_usedItemsCount == 0)
                    {
                        icon = cell.GetChild(4);
                        image = icon.GetComponent<Image>();
                        image.enabled = true;
                    }
                    _usedItemsCount += 1;
                }
                else
                {
                    icon = cell.GetChild(3);
                    image = icon.GetComponent<Image>();
                    StartCoroutine(ItemDidntUseCoroutine(image));
                }
            }
        }
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
        _usedItemsCount = 0;
    }

    private void OnEnable()
    {
        PlayerInputReader.OnInventoryTriggered += InventoryOpen;
        PlayerInputReader.OnSpellTriggered += SpellUsed;

        InventoryInputReader.OnCloseInventoryTriggered += InventoryOpen;

        ItemCollect.OnItemCollected += ItemCollected;

        CookingInputReader.OnIngredient1Triggered += ItemUsed;
        CookingInputReader.OnIngredient2Triggered += ItemUsed;
        CookingInputReader.OnIngredient3Triggered += ItemUsed;
        CookingInputReader.OnIngredient4Triggered += ItemUsed;
        CookingInputReader.OnIngredient5Triggered += ItemUsed;
        CookingInputReader.OnIngredient6Triggered += ItemUsed;
    }

    private void OnDisable()
    {
        PlayerInputReader.OnInventoryTriggered -= InventoryOpen;
        PlayerInputReader.OnSpellTriggered -= SpellUsed;

        InventoryInputReader.OnCloseInventoryTriggered -= InventoryOpen;

        ItemCollect.OnItemCollected -= ItemCollected;

        CookingInputReader.OnIngredient1Triggered -= ItemUsed;
        CookingInputReader.OnIngredient2Triggered -= ItemUsed;
        CookingInputReader.OnIngredient3Triggered -= ItemUsed;
        CookingInputReader.OnIngredient4Triggered -= ItemUsed;
        CookingInputReader.OnIngredient5Triggered -= ItemUsed;
        CookingInputReader.OnIngredient6Triggered -= ItemUsed;
    }
}
