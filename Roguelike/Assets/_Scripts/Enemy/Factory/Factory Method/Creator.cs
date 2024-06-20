using System.Collections.Generic;
using UnityEngine;

public abstract class Creator
{
    public abstract List<IDamageable> FactoryMethod(int amountToSpawn, Vector3 positionToSpawn, float spawnRange);
}
