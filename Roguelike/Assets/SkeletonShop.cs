using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonShop : MonoBehaviour
{
    [SerializeField] private GameObject _shopPanel;

    private bool _isShopOpen;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.T) && other.TryGetComponent(out Player player))
        {
            if (!_isShopOpen)
                _shopPanel.SetActive(true);
            else
                _shopPanel.SetActive(false);
        }
    }
}