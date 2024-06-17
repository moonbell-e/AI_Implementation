using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperSceneSpell : MonoBehaviour
{
    public float _damage;
    private float _actionTime;
    private int _passiveId1;
    private int _passiveId2;
    private int _passiveId3;
    private float _checkDelayTimer = 0f;
    private float _delayTime;
    private Transform _spellPosition;

    private bool _takeDamage = false;
    private List<IDamageable> _enemies = new();

    internal void Init(PepperConfig config, int id1, int id2, int id3, Transform spellPosition)
    {
        _damage = config.DamegeCount;
        _actionTime = config.ActionTime;
        _delayTime = config.DelayTime;
        _passiveId1 = id1;
        _passiveId2 = id2;
        _passiveId3 = id3;
        _spellPosition = spellPosition;
        Destroy(gameObject, 2f);
    }

    private void FixedUpdate()
    {
        transform.position = _spellPosition.position;
        transform.rotation = _spellPosition.rotation;

        _checkDelayTimer += Time.deltaTime;

        if (_checkDelayTimer >= _delayTime)
        {
            foreach (IDamageable enemy in _enemies)
            {
                if (enemy.gameObject.TryGetComponent(out StatusAbilityHandler handler))
                {
                    handler.OnSpellHit(_passiveId1, _passiveId2, _passiveId3);
                }
                enemy.TakeDamage(_damage);
            }

            _checkDelayTimer = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable idamageable))
        {
            _enemies.Add(idamageable);
        }


        /*if (other.GetComponent<BaseEnemy>())
        {
            _enemies.Add(other.GetComponent<BaseEnemy>());
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageable idamageable))
        {
            _enemies.Remove(idamageable);
        }

        /*if (other.GetComponent<BaseEnemy>())
        {
            _enemies.Remove(other.GetComponent<BaseEnemy>());
        }*/
    }
}
