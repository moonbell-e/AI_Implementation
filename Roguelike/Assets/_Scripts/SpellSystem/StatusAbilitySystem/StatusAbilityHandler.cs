using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusAbilityHandler : MonoBehaviour
{
    [SerializeField] StatusAbilityStorage _statusAbilityStorage;

    private List<StatusAbility> _statusAbilities = new();

    private void Awake()
    {
        _statusAbilityStorage.Init();
        _statusAbilities.AddRange(_statusAbilityStorage.GetStatusAbilities());
    }

    public void OnSpellHit(int statusId1, int statusId2, int statusId3)
    {
        _statusAbilities[statusId1].StartCast();
        _statusAbilities[statusId2].StartCast();
        _statusAbilities[statusId3].StartCast();
    }

    private void Update()
    {
        foreach (var statusAbility in _statusAbilities) { statusAbility.Update(); }
    }
}