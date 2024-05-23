using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCastHandler : MonoBehaviour
{
    [SerializeField] SpellStorage _spellStorage;
    [SerializeField] private SpellSystem _spellSystem;
    [SerializeField] PlayerHealthSystem _playerHealth;

    protected SpellSystem SpellSystem => _spellSystem;

    private List<Spell> _spells = new();
    private Spell _currentSpell;

    private void Awake()
    {
        _spellStorage.Init();
        _spells.AddRange(_spellStorage.GetSpells());
    }

    public void OnSpellCasted (int abilityId1, int abilityId2, int abilityId3)
    {
        _currentSpell = _spells[abilityId1];
        _currentSpell.StartCast(abilityId1, abilityId2, abilityId3);
    }

    private void OnEnable()
    {
        SpellSystem.OnSpellCasted += OnSpellCasted;
    }

    private void OnDisable()
    {
        SpellSystem.OnSpellCasted -= OnSpellCasted;
    }
}
