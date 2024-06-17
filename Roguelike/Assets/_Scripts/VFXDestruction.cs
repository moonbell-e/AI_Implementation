using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXDestruction : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
}
