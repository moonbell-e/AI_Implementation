using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ItemCollect _itemCollect;

    List<Item> items;

    public GameObject _inventoryContainer;
    public Animator _inventoryAnimator;

    protected InputReaderSwitcher InputReaderSwitcher => _player.InputReaderSwitcher;
    protected PlayerInputReader PlayerInputReader => _player.PlayerInputReader;
    protected CookingInputReader CookingInputReader => _player.CookingInputReader;
    protected InventoryInputReader InventoryInputReader => _player.InventoryInputReader;
    protected ItemCollect ItemCollect => _itemCollect;

    private void Start()
    {
        items = new List<Item>();
        for (int i = 0; i < _inventoryContainer.transform.childCount; i++) 
        {
            items.Add(new Item());
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
                InputReaderSwitcher.CookingModeEnable();
            }
        }
        else
        {
            if (_inventoryAnimator.GetCurrentAnimatorStateInfo(0).IsName("CookingOpened"))
            {
                _inventoryAnimator.SetTrigger("CookingClose");
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

    private void OnEnable()
    {
        PlayerInputReader.OnInventoryTriggered += InventoryOpen;
        InventoryInputReader.OnCloseInventoryTriggered += InventoryOpen;
        ItemCollect.OnItemCollected += ItemCollected;
    }

    private void OnDisable()
    {
        PlayerInputReader.OnInventoryTriggered -= InventoryOpen;
        InventoryInputReader.OnCloseInventoryTriggered -= InventoryOpen;
        ItemCollect.OnItemCollected -= ItemCollected;
    }
}
