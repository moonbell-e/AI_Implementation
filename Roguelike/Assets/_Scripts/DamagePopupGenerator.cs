using TMPro;
using UnityEngine;

public class DamagePopupGenerator : MonoBehaviour
{
    [SerializeField] private Transform _damagePopupPrefab;

    public void Create(Vector3 position, int damageAmount, bool isCritical)
    {
        var damagePopupTransform = Instantiate(_damagePopupPrefab, position, Quaternion.identity);
        
        var damagePopup = damagePopupTransform.GetComponentInChildren<DamagePopup>();
        damagePopup.Setup(damageAmount, isCritical);
    }
}