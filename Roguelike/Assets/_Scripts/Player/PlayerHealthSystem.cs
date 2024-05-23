using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
   [SerializeField] private float _health;
   [SerializeField] private float _maxHealth = 30f;
   [SerializeField] private Slider _slider;
   [SerializeField] private CameraShake _cameraShake;

   private void Start()
   {
      _health = _maxHealth;
      _slider.maxValue = _maxHealth;
      _slider.value = _health;
   }

   public void TakeDamage(float damageAmount)
   {
      _cameraShake.ShakeCamera(0.4f, 0.5f);
      _health -= damageAmount;
      _slider.value = _health;
      if (_health <= 0)
      {
         Die();
      }
   }

    public void TakeHeal(float healAmount)
    {
        _health += healAmount;
        if (_health >= _maxHealth) 
        {
            _health = _maxHealth;
        }
        _slider.value = _health;
    }

   private void Die()
   {
      Destroy(gameObject);
   }
}
