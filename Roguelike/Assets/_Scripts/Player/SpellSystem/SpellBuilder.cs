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
            _spell.SetDiscription(_spellConfig.Id, _spellConfig.Title, _spellConfig.Description, _spellConfig.IsOnEnemy);
            _spell.SetCooldownPoints(_spellConfig.CooldownPoints);
        }
    }

    public virtual Spell GetResult() => _spell;
}
