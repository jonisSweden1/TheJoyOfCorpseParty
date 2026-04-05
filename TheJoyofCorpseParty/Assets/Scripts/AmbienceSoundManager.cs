using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbienceSoundManager : MonoBehaviour
{
    public static AmbienceSoundManager Instance { get; private set; }

    [SerializeField]
    private AudioClip[] _ambienceClips;

    [SerializeField]
    private AudioClip[] _chaseMusicClips;

    private AudioSource _source;

    void Awake()
    {
        _source = GetComponent<AudioSource>();

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        PlayAmbience();
    }

    public void PlayAmbience()
    {
        if (_ambienceClips != null)
        {
            int random = Random.Range(0, _ambienceClips.Length - 1);
            _source.clip = _ambienceClips[random];
            _source.Play();
            _source.loop = true;
        }
    }

    public bool CheckIfAmbienceSoundIsPlaying()
    {
        if(_source.isPlaying)
        {
            // List of ambience clips to check if the current clip is one of them
            foreach (AudioClip clip in _ambienceClips)
            {
                if (_source.clip == clip)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void PauseMusic()
    {
        _source.Pause();
    }

    public void ResumeMusic()
    {
        _source.UnPause();
    }

    public void PlayChaseMusic()
    {
        if(_chaseMusicClips != null)
        {
            int random = Random.Range(0, _chaseMusicClips.Length - 1);
            _source.clip = _chaseMusicClips[random];
            _source.Play();
            _source.loop = true;
        }
    }
}
