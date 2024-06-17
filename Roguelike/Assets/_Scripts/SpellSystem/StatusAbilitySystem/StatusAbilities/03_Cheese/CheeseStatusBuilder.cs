using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseStatusBuilder : StatusAbilityBuilder
{
    private readonly CheeseStatusConfig _config;

    public CheeseStatusBuilder(CheeseStatusConfig config) : base(config)
    {
        _config = config;
    }

    public override void Make()
    {
        _statusAbility = new CheeseStatusAbility(_config);

        base.Make();
    }
}
