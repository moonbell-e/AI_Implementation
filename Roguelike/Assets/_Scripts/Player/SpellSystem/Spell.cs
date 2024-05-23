using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public int Id {  get; private set; }
    public string Title {  get; private set; }
    public string Description { get; private set; }
    public bool IsOnEnemy { get; private set; }
    public int CooldownPoints { get; private set; }


    public void SetDiscription(int id, string title, string description, bool isOnEnemy)
    {
        Id = id;
        Title = title;
        Description = description;
        IsOnEnemy = isOnEnemy;
    }

    public int SetCooldownPoints(int cooldownPoints) => CooldownPoints = cooldownPoints;

    public virtual void Added(Transform spellPosition) { }
    public virtual void StartCast(int id1, int id2, int id3) { }
}
