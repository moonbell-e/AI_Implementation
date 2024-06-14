using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]
public class TargetSensor : MonoBehaviour
{
    private SphereCollider _collider;

    public delegate void TargetEnterEvent(Transform target);

    public delegate void TargetExitEvent(Transform target);

    public event TargetEnterEvent OnTargetEnter;
    public event TargetEnterEvent OnTargetExit;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PotentialAttacker _))
            OnTargetEnter?.Invoke(other.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PotentialAttacker _))
            OnTargetEnter?.Invoke(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PotentialAttacker _))
            OnTargetExit?.Invoke(other.transform);
    }
}