using UnityEngine;

public class Robot : AggressiveEnemy
{
    [SerializeField] private RobotView _robotView;
    [SerializeField] private GameObject _loot;
    
    public RobotView RobotView => _robotView;

    protected override void Awake()
    {
        base.Awake();
        
        _robotView.Initialize();
    }

    public override void Die()
    {
        Destroy(gameObject);
    }


    public void StartDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
    }

    public void EndDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
    }
}
