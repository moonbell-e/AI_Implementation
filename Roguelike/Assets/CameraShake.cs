using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCam;
    private CinemachineBasicMultiChannelPerlin _perlinNoise;

    private void Awake()
    {
        _virtualCam = GetComponent<CinemachineVirtualCamera>();
        _perlinNoise = _virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        ResetIntensity();
    }

    public void ShakeCamera(float intensity, float shakeTime)
    {
        _perlinNoise.m_AmplitudeGain = intensity;
        StartCoroutine(WaitTime(shakeTime));
    }

    private IEnumerator WaitTime(float shakeTime)
    {
        yield return new WaitForSeconds(shakeTime);
        ResetIntensity();
    }

    private void ResetIntensity()
    {
        _perlinNoise.m_AmplitudeGain = 0f;
    }
}
