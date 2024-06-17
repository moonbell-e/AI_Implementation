using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenLegStatusBuilder : StatusAbilityBuilder
{
    private readonly ChickenLegStatusConfig _config;

    public ChickenLegStatusBuilder(ChickenLegStatusConfig config) : base(config)
    {
        _config = config;
    }

    public override void Make()
    {
        _statusAbility = new ChickenLegStatusAbility(_config);

        base.Make();
    }
}
