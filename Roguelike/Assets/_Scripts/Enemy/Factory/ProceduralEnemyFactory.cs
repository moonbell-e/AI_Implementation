using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ProceduralEnemyFactory : MonoBehaviour
{
    private List<IDamageable> _robots;
    private List<IDamageable> _predators;
    private List<IDamageable> _aggressiveHerbals;


    private void Awake()
    {
        _robots = new List<IDamageable>();
        _predators = new List<IDamageable>();
        _aggressiveHerbals = new List<IDamageable>();
    }

    public void CreateRobots(int count, Vector3 spawnPos, float spawnRange)
    {
        Creator robotsCreator = new RobotCreator();
        _robots = robotsCreator.FactoryMethod(count, spawnPos, spawnRange);
    }

    public void CreatePredators(int count, Vector3 spawnPos, float spawnRange, Transform restingPosition, Transform foodPosition)
    {
        PredatorCreator predatorsCreator = new PredatorCreator();
        _predators = predatorsCreator.FactoryMethod(count, spawnPos, spawnRange);

        foreach (var goapAgent in predatorsCreator.GetGoapAgents())
        {
            goapAgent.SetNeededPositions(restingPosition, foodPosition);
        }
    }

    public void CreateAggressiveHerbals(int count, Vector3 spawnPos, float spawnRange, Transform restingPosition, Transform foodPosition)
    {
        AggressiveHerbalCreator aggressiveHerbalsCreator = new AggressiveHerbalCreator();
        _aggressiveHerbals = aggressiveHerbalsCreator.FactoryMethod(count, spawnPos, spawnRange);

        foreach (var goapAgent in aggressiveHerbalsCreator.GetGoapAgents())
        {
            goapAgent.SetNeededPositions(restingPosition, foodPosition);
        }
    }
}