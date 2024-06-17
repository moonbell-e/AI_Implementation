using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoBuilder : SpellBuilder
{
    private readonly TomatoConfig _config;

    public TomatoBuilder(TomatoConfig config) : base(config)
    {
        _config = config;
    }

    public override void Make()
    {
        _spell = new TomatoSpell(_config);

        base.Make();
    }
}
