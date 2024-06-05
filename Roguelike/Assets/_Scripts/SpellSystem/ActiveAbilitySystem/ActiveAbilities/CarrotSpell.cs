using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class CarrotSpell : Spell
{
    private CarrotConfig _config;

    private Transform _spellPosition;

    public CarrotSpell(SpellConfig config)
    {
        _config = (CarrotConfig)config;
    }

    public override void Added(Transform spellPosition)
    {
        _spellPosition = spellPosition;
    }

    public override void StartCast(int id1, int id2, int id3)
    {
        GameObject.Instantiate(_config.CarrotSceneSpellPrefab, _spellPosition.position, _spellPosition.rotation).Init(_config, id1, id2, id3);
    }
}
