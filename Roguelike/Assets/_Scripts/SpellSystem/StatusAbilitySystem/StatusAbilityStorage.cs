using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusAbilityStorage : MonoBehaviour
{
    [SerializeField] private StatusAbilityConfig[] _statusAbilityConfig;
    [SerializeField] private GameObject _enemyObject;

    private List<StatusAbility> _statusAbilities = new();

    public void Init()
    {
        for (int i = 0; i < _statusAbilityConfig.Length; ++i)
        {
            var builder = _statusAbilityConfig[i].GetBuilder();

            builder.Make();
            var statusAbility = builder.GetResult();

            statusAbility.Added(_enemyObject);

            _statusAbilities.Add(statusAbility);
        }
    }

    public StatusAbility[] GetStatusAbilities() => _statusAbilities.ToArray();
}
