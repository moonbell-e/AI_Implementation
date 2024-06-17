using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseSpell : Spell
{
    private CheeseConfig _config;

    private Transform _spellPosition;

    public CheeseSpell(SpellConfig config)
    {
        _config = (CheeseConfig)config;
    }

    public override void Added(Transform spellPosition)
    {
        _spellPosition = spellPosition;
    }

    public override void StartCast(int id1, int id2, int id3)
    {
        GameObject.Instantiate(_config.CheeseSceneSpellPrefab, _spellPosition.position, _spellPosition.rotation).Init(_config, id1, id2, id3);
    }
}
