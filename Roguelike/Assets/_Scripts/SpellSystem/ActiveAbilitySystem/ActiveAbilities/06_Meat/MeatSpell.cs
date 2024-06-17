using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatSpell : Spell
{
    private MeatConfig _config;

    private Transform _spellPosition;

    public MeatSpell(SpellConfig config)
    {
        _config = (MeatConfig)config;
    }

    public override void Added(Transform spellPosition)
    {
        _spellPosition = spellPosition;
    }

    public override void StartCast(int id1, int id2, int id3)
    {
        GameObject.Instantiate(_config.MeatSceneSpellPrefab, _spellPosition.position, _spellPosition.rotation).Init(_config, id1, id2, id3);
    }
}
