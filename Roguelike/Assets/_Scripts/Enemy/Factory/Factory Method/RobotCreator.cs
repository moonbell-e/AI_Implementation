using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCreator : Creator
{
    public override List<IDamageable> FactoryMethod(int amountToSpawn, Vector3 positionToSpawn, float spawnRange)
    {
        List<IDamageable> spawnedEnemies = new List<IDamageable>();

        for (int i = 0; i < amountToSpawn; i++)
        {
            var prefab = Resources.Load<GameObject>("Prefabs/Enemies/Robot");

            Vector3 spawnPosition = positionToSpawn + new Vector3(
                Random.Range(-spawnRange, spawnRange),
                0,
                Random.Range(-spawnRange, spawnRange)
            );

            var go = GameObject.Instantiate(prefab, spawnPosition, Quaternion.identity);

            var enemyComponent = go.GetComponent<IDamageable>();
            spawnedEnemies.Add(enemyComponent);
        }

        return spawnedEnemies;
    }
}
