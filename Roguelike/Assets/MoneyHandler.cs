using TMPro;
using UnityEngine;

public class MoneyHandler : MonoBehaviour
{
    [SerializeField] private int _moneyCount;
    [SerializeField] private int _crystalsCount;

    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _crystalText;
    
    public int MoneyCount => _moneyCount;
    public int CrystalsCount => _crystalsCount;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        _moneyText.text = _moneyCount.ToString();
        _crystalText.text = _crystalsCount.ToString();
    }
    
    public void AddMoney(int money)
    {
        _moneyCount += money;
        _moneyText.text = _moneyCount.ToString();
    }
    
    public void AddCrystals(int crystals)
    {
        _crystalsCount += crystals;
        _crystalText.text = _crystalsCount.ToString();
    }
    
    public void SpendMoney(int money)
    {
        _moneyCount -= money;
        _moneyText.text = _moneyCount.ToString();
    }
    
    public void SpendCrystals(int crystals)
    {
        _crystalsCount -= crystals;
        _crystalText.text = _crystalsCount.ToString();
    }
}
