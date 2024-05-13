using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemCollect : MonoBehaviour
{
    [SerializeField] private Player _player;

    protected PlayerInputReader PlayerInputReader => _player.PlayerInputReader;

    private bool _triggered = false;

    public Action<Item> OnItemCollected;

    public void OnTriggerStay(Collider other)
    {
        if (_triggered)
        {
            if (other.gameObject.TryGetComponent(out Item item))
            {
                OnItemCollected.Invoke(item);
                Destroy(other.gameObject);
                _triggered = false;
            }
        }
    }

    public void ItemCollected()
    {
        StartCoroutine(ItemCollectedCoroutine());
    }

    IEnumerator ItemCollectedCoroutine()
    {
        _triggered = true;
        yield return new WaitForSeconds(0.1f);
        _triggered = false;
    }

    private void OnEnable()
    {
        PlayerInputReader.OnInteractTriggered += ItemCollected;
    }

    private void OnDisable()
    {
        PlayerInputReader.OnInteractTriggered -= ItemCollected;
    }
}
