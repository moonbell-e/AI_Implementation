using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeliefFactory
{
   private readonly BaseGoapAgent _agent;
   private readonly Dictionary<string, AgentBelief> _beliefs;
   
   public BeliefFactory(BaseGoapAgent agent, Dictionary<string, AgentBelief> beliefs)
   {
      _agent = agent;
      _beliefs = beliefs;
   }
   
   public void AddBelief(string key, Func<bool> condition)
   {
      _beliefs.Add(key, new AgentBelief.Builder(key)
         .WithCondition(condition)
         .Build());
   }

   private bool InRangeOf(Vector3 pos, float range) => Vector3.Distance(_agent.transform.position, pos) < range;
   
   public void AddSensorBelief(string key, Sensor sensor)
   {
      _beliefs.Add(key, new AgentBelief.Builder(key)
         .WithCondition(() => sensor.IsTargetInRange)
         .WithLocation(() => sensor.TargetPosition)
         .Build());
   }
   
   public void AddLocationBelief(string key, float distance, Transform locationCondition)
   {
      AddLocationBelief(key, distance, locationCondition.position);
   }

   private void AddLocationBelief(string key, float distance, Vector3 locationCondition)
   {
      _beliefs.Add(key, new AgentBelief.Builder(key)
         .WithCondition(() => InRangeOf(locationCondition, distance))
         .WithLocation(() => locationCondition)
         .Build());
   }
   
   
}
public class AgentBelief 
{
   public string Name { get; }

   private Func<bool> _condition = () => false;
   private Func<Vector3> _observedLocation = () => Vector3.zero;

   public Vector3 Location => _observedLocation();

   private AgentBelief(string name)
   {
      Name = name;
   }
   
   public bool Evaluate() => _condition();

   public class Builder
   {
      private readonly AgentBelief _belief;
      
      public Builder(string name)
      {
         _belief = new AgentBelief(name);
      }
      
      public Builder WithCondition(Func<bool> condition)
      {
         _belief._condition = condition;
         return this;
      }
      
      public Builder WithLocation(Func<Vector3> location)
      {
         _belief._observedLocation = location;
         return this;
      }

      public AgentBelief Build()
      {
         return _belief;
      }
   }
}
