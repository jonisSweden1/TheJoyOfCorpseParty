using System;
using UnityEngine;

public class EnemySoundDetection : MonoBehaviour
{
    [SerializeField]
    private float _hearingRange;

    [SerializeField]
    private Transform _headTransform;

    public event Action<EHeardSoundCategory, float> onHeardSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HearingManager.instance.Register(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if(HearingManager.instance != null)
            HearingManager.instance.Unregister(this);
    }

    public void OnHeardSound(Vector3 location, EHeardSoundCategory category, float intensity)
    {
        if(Vector3.Distance(location, _headTransform.position) < _hearingRange)
        {
            onHeardSound.Invoke(category, intensity);
        }
    }
}
