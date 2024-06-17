using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class TomatoSceneSpell : MonoBehaviour
{
    [SerializeField] private GameObject _destroyVFX;

    public float _damage;
    private float _speed;
    private float _actionTime;
    private int _passiveId1;
    private int _passiveId2;
    private int _passiveId3;
    internal void Init(TomatoConfig config, int id1, int id2, int id3)
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

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.GetComponent<Player>())
        {
            GameObject.Instantiate(_destroyVFX, gameObject.transform.position, gameObject.transform.rotation);
            gameObject.GetComponent<SphereCollider>().enabled = true;
            Destroy(gameObject, 0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StatusAbilityHandler handler))
        {
            handler.OnSpellHit(_passiveId1, _passiveId2, _passiveId3);
        }
        if (other.TryGetComponent(out IDamageable idamageable))
        {
            idamageable.TakeDamage(_damage);
        }
    }
}
