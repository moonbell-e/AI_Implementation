using UnityEngine;

public class HealthSystem : MonoBehaviour
{
   [SerializeField] private float _health = 100f;

   public void TakeDamage(float damageAmount)
   {
      _health -= damageAmount;
      if (_health <= 0)
      {
         Die();
      }
   }

   private void Die()
   {
      Destroy(gameObject);
   }
}
