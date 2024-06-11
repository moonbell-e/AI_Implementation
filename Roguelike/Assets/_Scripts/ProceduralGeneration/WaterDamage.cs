using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDamage : MonoBehaviour
{
    float CheckDelayTimer = 0f;
    float takenDamage = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            takenDamage = 0.5f;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            CheckDelayTimer += Time.deltaTime;

            if (CheckDelayTimer >= 1f)
            {
                takenDamage += 0.25f;
                if (other.GetComponent<PlayerHealthSystem>())
                {
                    other.GetComponent<PlayerHealthSystem>().TakeDamage(takenDamage);
                }
                CheckDelayTimer = 0.0f;
            }
        }
    }
}
