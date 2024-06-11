using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusAbilityBuilder
{
    private StatusAbilityConfig _statusAbilityConfig;
    protected StatusAbility _statusAbility;

    public StatusAbilityBuilder(StatusAbilityConfig statusAbilityConfig)
    {
        _statusAbilityConfig = statusAbilityConfig;
    }

    public virtual void Make()
    {
        if (_statusAbility != null)
        {
            _statusAbility.SetStatusAbility(_statusAbilityConfig.ActionTime, _statusAbilityConfig.DelayTime, _statusAbilityConfig.DamegeCount);
        }
    }

    public virtual StatusAbility GetResult() => _statusAbility;
}