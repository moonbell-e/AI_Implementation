using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damageAmount);

    void Die();

    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }
    GameObject gameObject { get; }

}
