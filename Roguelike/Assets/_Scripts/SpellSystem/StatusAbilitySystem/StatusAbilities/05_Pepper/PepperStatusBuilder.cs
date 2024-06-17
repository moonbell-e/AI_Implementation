using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperStatusBuilder : StatusAbilityBuilder
{
    private readonly PepperStatusConfig _config;

    public PepperStatusBuilder(PepperStatusConfig config) : base(config)
    {
        _config = config;
    }

    public override void Make()
    {
        _statusAbility = new PepperStatusAbility(_config);

        base.Make();
    }
}
