using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentGoal
{
    public string Name {get;}
    public float Priority { get; private set; }
    public HashSet<AgentBelief> DesiredEffects { get; } = new();

    private AgentGoal(string name)
    {
        Name = name;
    }
    
    public class Builder {
        readonly AgentGoal _goal;

        public Builder(string name) {
            _goal = new AgentGoal(name);
        }
        
        public Builder WithPriority(float priority) {
            _goal.Priority = priority;
            return this;
        }

        public Builder WithDesiredEffect(AgentBelief effect) {
            _goal.DesiredEffects.Add(effect);
            return this;
        }

        public AgentGoal Build() {
            return _goal;
        }
    }
}
