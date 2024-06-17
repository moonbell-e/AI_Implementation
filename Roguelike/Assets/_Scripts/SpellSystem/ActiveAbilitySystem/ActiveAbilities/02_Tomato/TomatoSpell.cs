using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoSpell : Spell
{
    private TomatoConfig _config;

    private Transform _spellPosition;

    public TomatoSpell(SpellConfig config)
    {
        _config = (TomatoConfig)config;
    }

    public override void Added(Transform spellPosition)
    {
        _spellPosition = spellPosition;
    }

    public override void StartCast(int id1, int id2, int id3)
    {
        GameObject.Instantiate(_config.TomatoSceneSpellPrefab, _spellPosition.position, _spellPosition.rotation).Init(_config, id1, id2, id3);
    }
}
