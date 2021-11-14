using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UD_ScreenShake : MonoBehaviour
{
    // Place this script on character

    [Header("Shake Parameters")]
    public float shakeDuration;
    public float shakeAmplitude;
    public float shakeFrequency;
    
    float currentShakeAmplitude;
    float currentShakeFrequency;

    private float ShakeElapsedTime = 0f;

    // Cinemachine Shake
    private CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    void Start()
    {
        VirtualCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineVirtualCamera>();
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        // TODO: Replace with your trigger
        /*if (Input.GetKey(KeyCode.W))
        {
            StartShake();
        }*/

        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = currentShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = currentShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }
    }

    public void StartShake()
    {
        ShakeElapsedTime = shakeDuration;
        currentShakeAmplitude = shakeAmplitude;
        currentShakeFrequency = shakeFrequency;
    }
}
