using UnityEngine;

public class GhostEnemy : BaseEnemy
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private float _ballSpeed;

    protected override void Update()
    {
        if (_timePassed >= _attackCooldown)
        {
            ShootAtPlayer();
            _timePassed = 0;
        }

        _timePassed += Time.deltaTime;

        if (_newDestinationCooldown <= 0 &&
            Vector3.Distance(_player.transform.position, transform.position) <= _aggroRange)
        {
            _agent.SetDestination(_player.transform.position);
            _newDestinationCooldown = 0.5f;
        }

        _newDestinationCooldown -= Time.deltaTime;
         transform.LookAt(_player.transform);
    }

    private void ShootAtPlayer()
    {
        var spawnPointTransform = _spawnPoint.transform;
        var ballObj = Instantiate(_ballPrefab, spawnPointTransform.position, spawnPointTransform.rotation);
        var ballRig = ballObj.GetComponent<Rigidbody>();
        ballRig.AddForce(spawnPointTransform.forward * _ballSpeed, ForceMode.Impulse);
        Destroy(ballObj, 5f);
    }
}