using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _walkingFootStepAudioClips, _runningFootStepAudioClips;

    private AudioSource _audioSource;

    [SerializeField]
    private float runBetweenTime, walkBetweenTime;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if()
    }
}
