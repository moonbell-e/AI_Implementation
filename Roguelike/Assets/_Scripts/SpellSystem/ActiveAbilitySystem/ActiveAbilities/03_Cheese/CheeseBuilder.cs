using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseBuilder : SpellBuilder
{
    private readonly CheeseConfig _config;

    public CheeseBuilder(CheeseConfig config) : base(config)
    {
        _config = config;
    }

    public override void Make()
    {
        _spell = new CheeseSpell(_config);

        base.Make();
    }
}
