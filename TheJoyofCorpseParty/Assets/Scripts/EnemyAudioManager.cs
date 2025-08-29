using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private EnemyStateManager _enemyStateManager;

    [SerializeField]
    private AudioClip[] _walkingFootStepsAudioClips, _runningFootStepsAudioClips;

    [SerializeField]
    private AudioClip _chaseSignalAudioClip;

    private bool _hasChasePlayed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _enemyStateManager = GetComponent<EnemyStateManager>();
    }

    private void Start()
    {
        _hasChasePlayed = false;
    }

    private void OnEnable()
    {
        _enemyStateManager.onStateChanged += EnemyStateManager_OnStateChanged;
    }

    private void OnDisable()
    {
        _enemyStateManager.onStateChanged -= EnemyStateManager_OnStateChanged;
    }

    private void EnemyStateManager_OnStateChanged()
    {
        if(!_hasChasePlayed)
        {
            if (_enemyStateManager.CheckStateWithState(_enemyStateManager.chaseState))
            {
                if (_chaseSignalAudioClip)
                {
                    _audioSource.PlayOneShot(_chaseSignalAudioClip);
                    _hasChasePlayed = true;
                }
            }
        }
        else
        {
            if(_enemyStateManager.CheckStateWithState(_enemyStateManager.roamState))
            {
                _hasChasePlayed = false;
            }
        }
    }

    public void PlayStepSound()
    {
        if(_walkingFootStepsAudioClips != null &&
            _runningFootStepsAudioClips != null)
        {
            if(_enemyStateManager.CheckStateWithState(_enemyStateManager.chaseState))
            {
                PlayRunningFootSteps();
            }
            else if(_enemyStateManager.CheckStateWithState(_enemyStateManager.roamState))
            {
                PlayWalkingFootSteps();
            }
        }
    }

    private void PlayRunningFootSteps()
    {
        int random = Random.Range(0, _runningFootStepsAudioClips.Length - 1);
        AudioClip audioClip = _runningFootStepsAudioClips[random];
        _audioSource.volume = 0.9f;
        _audioSource.PlayOneShot(audioClip);
    }

    private void PlayWalkingFootSteps()
    {
        int random = Random.Range(0, _walkingFootStepsAudioClips.Length - 1);
        AudioClip audioClip = _walkingFootStepsAudioClips[random];
        _audioSource.volume = 0.4f;
        _audioSource.PlayOneShot(audioClip);
    }
}
