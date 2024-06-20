using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Animator rotationAnimator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rotationAnimator.SetTrigger("Trigger");
        }
    }
}
