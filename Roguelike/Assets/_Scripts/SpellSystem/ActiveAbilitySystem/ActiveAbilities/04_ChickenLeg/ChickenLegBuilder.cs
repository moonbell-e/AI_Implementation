using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenLegBuilder : SpellBuilder
{
    private readonly ChickenLegConfig _config;

    public ChickenLegBuilder(ChickenLegConfig config) : base(config)
    {
        _config = config;
    }

    public override void Make()
    {
        _spell = new ChickenLegSpell(_config);

        base.Make();
    }
}
