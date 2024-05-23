using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarrotSceneSpell : MonoBehaviour
{
    public float _damage;
    private float _speed;
    private float _lifeTime;
    private int _passiveId1;
    private int _passiveId2;
    private int _passiveId3;
    internal void InIt(CarrotConfig config, int id1, int id2, int id3)
    {
        _damage = config.DamageCount;
        _speed = config.SpeedCount;
        _lifeTime = config.LifeTimeCount;
        _passiveId1 = id1;
        _passiveId2 = id2;
        _passiveId3 = id3;
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) 
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log(_passiveId1);
                Debug.Log(_passiveId2);
                Debug.Log(_passiveId3);
            }
            Destroy(gameObject);
        }
    }
}
