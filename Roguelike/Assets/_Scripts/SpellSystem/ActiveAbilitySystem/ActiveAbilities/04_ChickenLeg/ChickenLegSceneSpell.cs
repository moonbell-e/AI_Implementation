using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenLegSceneSpell : MonoBehaviour
{
    public float _damage;
    private float _speed;
    private float _actionTime;
    private int _passiveId1;
    private int _passiveId2;
    private int _passiveId3;
    internal void Init(ChickenLegConfig config, int id1, int id2, int id3)
    {
        _damage = config.DamegeCount;
        _speed = config.SpeedCount;
        _actionTime = config.ActionTime;
        _passiveId1 = id1;
        _passiveId2 = id2;
        _passiveId3 = id3;
        Destroy(gameObject, _actionTime);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Player>())
        {
            if (other.TryGetComponent(out StatusAbilityHandler handler))
            {
                handler.OnSpellHit(_passiveId1, _passiveId2, _passiveId3);
            }
            if (other.TryGetComponent(out IDamageable idamageable))
            {
                idamageable.TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
    }
}
