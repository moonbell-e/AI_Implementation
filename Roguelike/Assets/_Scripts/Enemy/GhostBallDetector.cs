using UnityEngine;

public class GhostBallDetector : MonoBehaviour
{
    [SerializeField] private float _damage = 5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthSystem player))
        {
            player.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
