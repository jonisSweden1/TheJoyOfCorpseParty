using System;
using UnityEngine;

public class EnemySoundDetection : MonoBehaviour
{
    [SerializeField]
    private float _hearingRange;

    public float HearingRange { get { return _hearingRange; } private set { _hearingRange = value; } }

    [SerializeField]
    private Transform _headTransform;

    public Transform HeadTransform { get { return _headTransform; } private set { _headTransform = value; } }

    public event Action<Vector3, EHeardSoundCategory, float> onHeardSound;

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

    public void OnHeardSound(AudioSource source, Vector3 location, EHeardSoundCategory category, float intensity)
    {
        if(Vector3.Distance(location, _headTransform.position) < _hearingRange)
        {
            onHeardSound.Invoke(location, category, intensity);
        }
    }
}
