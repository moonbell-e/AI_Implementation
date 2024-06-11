using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotBuilder : SpellBuilder
{
    private readonly CarrotConfig _config;

    public CarrotBuilder(CarrotConfig config) : base(config)
    {
        _config = config;
    }

    public override void Make()
    {
        _spell = new CarrotSpell(_config);

        base.Make();
    }
}
