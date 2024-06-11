using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private Transform _restingPosition;
    [SerializeField] private Transform _foodPosition;
    
    [Header("Robots")]
    [SerializeField] private int _robotsCount;
    [SerializeField] private Vector3 _robotsSpawnPos;
    [SerializeField] private float _robotSpawnRange;
    
    [Header("Predator")]
    [SerializeField] private int _predatorsCount;
    [SerializeField] private Vector3 _predatorsSpawnPos;
    [SerializeField] private float _predatorSpawnRange;

    [Header("AggressiveHerbal")] 
    [SerializeField] private int _aggressiveHerbalCount;
    [SerializeField] private Vector3 _aggressiveHerbalSpawnPos;
    [SerializeField] private float _aggressiveHerbalSpawnRange;
    

    private List<IDamageable> _robots;
    private List<IDamageable> _predators;
    private List<IDamageable> _aggressiveHerbals;


    private void Start()
    {
        _robots = new List<IDamageable>();
        _predators = new List<IDamageable>();
        _aggressiveHerbals = new List<IDamageable>();
        
        CreateRobots();
        CreatePredators();
        CreateAggressiveHerbals();
    }

    private void CreateRobots()
    {
        Creator robotsCreator = new RobotCreator();
        _robots = robotsCreator.FactoryMethod(_robotsCount, _robotsSpawnPos, _robotSpawnRange);
    }

    private void CreatePredators()
    {
        PredatorCreator predatorsCreator = new PredatorCreator();
        _predators = predatorsCreator.FactoryMethod(_predatorsCount, _predatorsSpawnPos, _predatorSpawnRange);
        
        foreach (var goapAgent in  predatorsCreator.GetGoapAgents())
        {
            goapAgent.SetNeededPositions(_restingPosition, _foodPosition);
        }
    }
    
    private void CreateAggressiveHerbals()
    {
        AggressiveHerbalCreator aggressiveHerbalsCreator = new AggressiveHerbalCreator();
        _aggressiveHerbals = aggressiveHerbalsCreator.FactoryMethod(_aggressiveHerbalCount, _aggressiveHerbalSpawnPos, _aggressiveHerbalSpawnRange);
        
        foreach (var goapAgent in  aggressiveHerbalsCreator.GetGoapAgents())
        {
            goapAgent.SetNeededPositions(_restingPosition, _foodPosition);
        }
    }
}
