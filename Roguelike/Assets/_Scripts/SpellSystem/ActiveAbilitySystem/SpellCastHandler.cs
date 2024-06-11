using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCastHandler : MonoBehaviour
{
    [SerializeField] SpellStorage _spellStorage;
    [SerializeField] private SpellManager _spellManager;
    [SerializeField] PlayerHealthSystem _playerHealth;

    protected SpellManager SpellManager => _spellManager;

    private List<Spell> _spells = new();

    private void Awake()
    {
        _spellStorage.Init();
        _spells.AddRange(_spellStorage.GetSpells());
    }

    public void OnSpellCasted (int abilityId1, int abilityId2, int abilityId3)
    {
        _spells[abilityId1].StartCast(abilityId1, abilityId2, abilityId3);
    }

    private void OnEnable()
    {
        SpellManager.OnSpellCasted += OnSpellCasted;
    }

    private void OnDisable()
    {
        SpellManager.OnSpellCasted -= OnSpellCasted;
    }
}
