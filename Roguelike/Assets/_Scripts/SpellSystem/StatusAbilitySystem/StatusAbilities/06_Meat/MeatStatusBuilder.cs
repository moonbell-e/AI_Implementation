using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatStatusBuilder : StatusAbilityBuilder
{
    private readonly MeatStatusConfig _config;

    public MeatStatusBuilder(MeatStatusConfig config) : base(config)
    {
        _config = config;
    }

    public override void Make()
    {
        _statusAbility = new MeatStatusAbility(_config);

        base.Make();
    }
}
