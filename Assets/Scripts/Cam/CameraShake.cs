using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera _VCam;
    public float shakeIntensity;
    public float shakeTime;
    public float timer;
    private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;
    public Neko2 neko2;
    // Start is called before the first frame update
    void Awake()
    {
        neko2 = FindObjectOfType<Neko2>();
       _VCam= GetComponent<CinemachineVirtualCamera>(); 
    }
    private void Start()
    {
        _VCam.Follow = neko2.transform;
        StopShake();
    }
    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin _multiChannelPerlin = _VCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _multiChannelPerlin.m_AmplitudeGain= shakeIntensity;
        timer= shakeTime;
    }
   public void StopShake()
    {
        CinemachineBasicMultiChannelPerlin _multiChannelPerlin = _VCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _multiChannelPerlin.m_AmplitudeGain = 0;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>0)
        {
            timer-=Time.deltaTime;
            if(timer<=0)
            {
                StopShake();
            }
        }
    }
}
