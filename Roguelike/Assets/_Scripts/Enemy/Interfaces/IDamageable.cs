using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damageAmount);

    void Die();

    void TakeDamageWithoutAnimation(float damageAmount);

    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }
    GameObject gameObject { get; }

}
