using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperBuilder : SpellBuilder
{
    private readonly PepperConfig _config;

    public PepperBuilder(PepperConfig config) : base(config)
    {
        _config = config;
    }

    public override void Make()
    {
        _spell = new PepperSpell(_config);

        base.Make();
    }
}
