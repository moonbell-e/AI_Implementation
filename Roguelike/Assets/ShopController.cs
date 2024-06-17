using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] private List<Button> _shopItems;
    [SerializeField] private List<int> _shopItemsPrices;
    [SerializeField] private MoneyHandler _moneyHandler;
    
    private void Start()
    {
        for (var i = 0; i < _shopItems.Count; i++)
        {
            var item = _shopItems[i];
            var price = _shopItemsPrices[i];
            int index = i;
            item.onClick.AddListener(() => {
                int buttonIndex = index; 
                BuyItem(price, buttonIndex);
            });
        }
    }

    private void BuyItem(int price, int index)
    {
        if (_moneyHandler.MoneyCount >= price)
        {
            _moneyHandler.SpendMoney(price);
            _shopItems[index].interactable = false;
        }
    }
}
