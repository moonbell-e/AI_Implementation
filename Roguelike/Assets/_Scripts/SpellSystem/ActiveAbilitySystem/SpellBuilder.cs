using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBuilder
{
    private SpellConfig _spellConfig;
    protected Spell _spell;

    public SpellBuilder(SpellConfig spellConfig)
    {
        _spellConfig = spellConfig;
    }

    public virtual void Make()
    {
        if (_spell != null)
        {
            _spell.SetCooldownPoints(_spellConfig.CooldownPoints);
            _spell.SetActiveAbility(_spellConfig.ActionTime, _spellConfig.SpeedCount, _spellConfig.DamegeCount);
        }
    }

    public virtual Spell GetResult() => _spell;
}
