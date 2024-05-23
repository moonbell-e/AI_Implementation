using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class SpellStorage : MonoBehaviour
{
    [SerializeField] private SpellConfig[] _spellConfigs;
    [SerializeField] private PlayerHealthSystem _owner;
    [SerializeField] private Transform _spellPosition;

    private List<Spell> _spells = new();

    public void Init()
    {
        for (int i = 0; i < _spellConfigs.Length; ++i)
        {
            var builder = _spellConfigs[i].GetBuilder();

            builder.Make();
            var spell = builder.GetResult();

            spell.Added(_spellPosition);

            _spells.Add(spell);
        }
    }

    public Spell[] GetSpells() => _spells.ToArray();
}
