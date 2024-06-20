using System.Collections.Generic;
using UnityEngine;

public class NHerbalsFactory : AbstractFactory
{
    public override List<IDamageable> CreateCactus(int amountToSpawn, Vector3 positionToSpawn, float spawnRange)
    {
        List<IDamageable> spawnedEnemies = new List<IDamageable>();

        for (int i = 0; i < amountToSpawn; i++)
        {
            var prefab = Resources.Load<GameObject>("Prefabs/Enemies/Cactus");

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

    public override List<IDamageable> CreateBurrow(int amountToSpawn, Vector3 positionToSpawn, float spawnRange)
    {
        List<IDamageable> spawnedEnemies = new List<IDamageable>();

        for (int i = 0; i < amountToSpawn; i++)
        {
            var prefab = Resources.Load<GameObject>("Prefabs/Enemies/Burrow");

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

    public override List<IDamageable> CreateMushroom(int amountToSpawn, Vector3 positionToSpawn, float spawnRange)
    {
        List<IDamageable> spawnedEnemies = new List<IDamageable>();

        for (int i = 0; i < amountToSpawn; i++)
        {
            var prefab = Resources.Load<GameObject>("Prefabs/Enemies/MushroomAngry");

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
