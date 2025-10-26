using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAudioManager : MonoBehaviour
{
    private AudioSource _audioSource;

    private NavMeshAgent _agent;

    [SerializeField]
    private AudioSource _audioSource2;

    [Header("Choose between state or behavior graph")]
    [SerializeField]
    private EnemyStateManager _enemyStateManager;

    [SerializeField]
    private BehaviorGraphAgent _behaviorGraphAgent;

    [Header("Speeds")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [Header("AudioClips")]
    [SerializeField] private AudioClip[] _walkingFootStepsAudioClips;
    [SerializeField] private AudioClip[] _runningFootStepsAudioClips;

    [SerializeField]
    private AudioClip _chaseSignalAudioClip;

    [SerializeField]
    private AudioClip _machineSound;

    [SerializeField]
    private AudioClip _startMachineSound;

    private bool _hasChasePlayed;
    private bool _hasAwaked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _hasChasePlayed = false;

        if(_enemyStateManager.IsAwake)
        {
            if (_audioSource2 != null)
            {
                _audioSource2.clip = _machineSound;
                _audioSource2.loop = true;
                _audioSource2.Play();
            }
        }
    }

    
    private void OnEnable()
    {
        if(_enemyStateManager != null)
            _enemyStateManager.onStateChanged += EnemyStateManager_OnStateChanged;
    }

    private void OnDisable()
    {
        if(_enemyStateManager != null)
            _enemyStateManager.onStateChanged -= EnemyStateManager_OnStateChanged;
    }

    private void EnemyStateManager_OnStateChanged()
    {
        if(_enemyStateManager.CheckStateWithState(_enemyStateManager.wakeState))
        {
            if (_audioSource2 != null)
            {
                if(!_hasAwaked)
                {
                    _audioSource2.PlayOneShot(_startMachineSound);
                }
            }
        }

        if(!_hasChasePlayed)
        {
            if(_enemyStateManager != null)
            {
                if (_enemyStateManager.CheckStateWithState(_enemyStateManager.chasePlayerState))
                {
                    if (_chaseSignalAudioClip != null)
                    {
                        _audioSource.PlayOneShot(_chaseSignalAudioClip);
                        _hasChasePlayed = true;
                    }
                }
            }
        }
        else
        {
            if(_enemyStateManager != null)
            {
                if (_enemyStateManager.CheckStateWithState(_enemyStateManager.roamState))
                {
                    _hasChasePlayed = false;
                }
            }
        }
    }
    
    /*
    private void CallChaseSignal()
    {
        if (_chaseSignalAudioClip != null)
        {
            _audioSource.PlayOneShot(_chaseSignalAudioClip);
            _hasChasePlayed = true;
        }
    }
    */

    public void PlayStepSound()
    {
        float halfSpeedOfRunCal = (runSpeed - walkSpeed) / 2;
        float halfRunSpeed = walkSpeed + halfSpeedOfRunCal;

        float walkSpeedHalf = walkSpeed / 2;

        if(_walkingFootStepsAudioClips != null &&
            _runningFootStepsAudioClips != null)
        {
            if(_agent.velocity.normalized.magnitude < halfRunSpeed)
            {
                PlayRunningFootSteps();
            }
            else if(_agent.velocity.normalized.magnitude < walkSpeedHalf)
            {
                PlayWalkingFootSteps();
            }
            else
            {

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
