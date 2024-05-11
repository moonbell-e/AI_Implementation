using UnityEngine;

public class HealthSystem : MonoBehaviour
{
   [SerializeField] private float _health = 100f;
   private PlayerView _playerView;

   private void Awake()
   {
      _playerView = GetComponent<PlayerView>();
   }

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
