using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public CinemachineFreeLook cfl;

    public static CameraShake Instance;

    float amplitude = 0;

    public void Awake()
    {
        Instance = this;
    }


    public void Update()
    {
        if (amplitude > 0)
        {
            amplitude -= 0.01f;
        }

        cfl.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        cfl.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        cfl.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
    }



    public void ShakeCamera()
    {

        amplitude = 2.4f;
    }

}
