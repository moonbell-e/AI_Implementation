using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperSpell : Spell
{
    private PepperConfig _config;

    private Transform _spellPosition;

    public PepperSpell(SpellConfig config)
    {
        _config = (PepperConfig)config;
    }

    public override void Added(Transform spellPosition)
    {
        _spellPosition = spellPosition;
    }

    public override void StartCast(int id1, int id2, int id3)
    {
        GameObject.Instantiate(_config.PepperSceneSpellPrefab, _spellPosition.position, _spellPosition.rotation).Init(_config, id1, id2, id3, _spellPosition);
    }
}
