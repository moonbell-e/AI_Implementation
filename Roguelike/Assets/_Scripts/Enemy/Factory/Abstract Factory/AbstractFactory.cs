
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFactory
{
    public abstract List<IDamageable> CreateCactus(int amountToSpawn, Vector3 positionToSpawn, float spawnRange);

    public abstract List<IDamageable> CreateBurrow(int amountToSpawn, Vector3 positionToSpawn, float spawnRange);

    public abstract List<IDamageable> CreateMushroom(int amountToSpawn, Vector3 positionToSpawn, float spawnRange);
}
