using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoStatusBuilder : StatusAbilityBuilder
{
    private readonly TomatoStatusConfig _config;

    public TomatoStatusBuilder(TomatoStatusConfig config) : base(config)
    {
        _config = config;
    }

    public override void Make()
    {
        _statusAbility = new TomatoStatusAbility(_config);

        base.Make();
    }
}
