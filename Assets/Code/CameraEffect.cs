using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CameraEffect : MonoBehaviour
{
    public float ShakeDuration = 0.3f;          // Time the Camera Shake effect will last
    public float ShakeAmplitude = 1.2f;         // Cinemachine Noise Profile Parameter
    public float ShakeFrequency = 2.0f;         // Cinemachine Noise Profile Parameter

    private float ShakeElapsedTime = 0f;

    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    // Use this for initialization
    void Start()
    {
        // Get Virtual Camera Noise Profile
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }


    public IEnumerator Shake()
    {
        ShakeElapsedTime = ShakeDuration;

        while (true)
        {
            if (ShakeElapsedTime <= 0)
            {
                virtualCameraNoise.m_AmplitudeGain = 0f;
                yield break;
            }
            yield return new WaitForEndOfFrame();
            if (VirtualCamera != null && virtualCameraNoise != null)
            {
                // If Camera Shake effect is still playing
                if (ShakeElapsedTime > 0)
                {
                    // Set Cinemachine Camera Noise parameters
                    virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                    virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                    // Update Shake Timer
                    ShakeElapsedTime -= Time.deltaTime;
                }
                else
                {
                    // If Camera Shake effect is over, reset variables
                    
                    ShakeElapsedTime = 0f;
                }
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        // TODO: Replace with your trigger
        if (Input.GetKey(KeyCode.S))
        {
            
        }

        // If the Cinemachine componet is not set, avoid update
        
    }
}
