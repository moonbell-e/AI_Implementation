using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_01_Animation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            GetComponent<Animator>().SetTrigger("DoorClose");
            GetComponent<Animator>().ResetTrigger("DoorOpen");
        }
    }
}
