using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMovable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 1f;
    public float CurrentHealth { get; set; }
    public Rigidbody RB { get; set; }
    
    private void Start()
    {
        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody>();
    }

    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0f)
            Die();
    }

    public void Die()
    {
       Destroy(gameObject);
    }
    
    public void MoveEnemy(Vector2 velocity)
    {
        RB.velocity = velocity;
    }
}
