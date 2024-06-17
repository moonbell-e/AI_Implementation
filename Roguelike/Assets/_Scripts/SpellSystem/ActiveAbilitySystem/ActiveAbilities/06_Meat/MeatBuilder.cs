using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatBuilder : SpellBuilder
{
    private readonly MeatConfig _config;

    public MeatBuilder(MeatConfig config) : base(config)
    {
        _config = config;
    }

    public override void Make()
    {
        _spell = new MeatSpell(_config);

        base.Make();
    }
}
