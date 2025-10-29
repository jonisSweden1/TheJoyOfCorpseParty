using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _walkingFootStepAudioClips, _runningFootStepAudioClips;

    private AudioSource _audioSource;
    private PlayerMovement _playerMovement;

    private Rigidbody _rb; //for reading

    [Header("Time between...")]
    [SerializeField] private float runBetweenTime;
    [SerializeField] private float walkBetweenTime;
    [SerializeField] private float crouchWalkBetweenTime;

    [Header("Volume scales")]

    [SerializeField] private float crouchVolumeScale;
    [SerializeField] private float noCrouchVolumeScale;

    private float time = 0;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(_walkingFootStepAudioClips == null)
        {
            Debug.LogError("There is no walking step sound effects! Thus, there will be no sound effects playing");
        }

        if(_runningFootStepAudioClips == null)
        {
            Debug.LogError("There is no running step sound effects! Thus, there will be no sound effects playing");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_rb.linearVelocity.magnitude > 0.1)
        {
            if(_playerMovement.currentState == MovementState.crouching)
            {
                time += Time.deltaTime;

                if (time > crouchWalkBetweenTime)
                {
                    if (_walkingFootStepAudioClips.Length > 0)
                    {
                        int randIndex = Random.Range(0, _walkingFootStepAudioClips.Length - 1);
                        _audioSource.PlayOneShot(_walkingFootStepAudioClips[randIndex], crouchVolumeScale);
                        HearingManager.instance.OnSoundEmitted(_audioSource, transform.position, EHeardSoundCategory.EFootstep, 0.1f);
                    }

                    time = 0;
                }
            }
            else if(_playerMovement.currentState == MovementState.sprinting)
            {
                time += Time.deltaTime;

                if(time > runBetweenTime)
                {
                    if(_runningFootStepAudioClips.Length > 0)
                    {
                        int randIndex = Random.Range(0, _runningFootStepAudioClips.Length - 1);
                        _audioSource.PlayOneShot(_runningFootStepAudioClips[randIndex], noCrouchVolumeScale);
                        HearingManager.instance.OnSoundEmitted(_audioSource, transform.position, EHeardSoundCategory.EFootstep, 0.5f);
                    }

                    time = 0;
                }
            }
            else if(_playerMovement.currentState == MovementState.walking)
            {
                time += Time.deltaTime;

                if(time > walkBetweenTime)
                {
                    if(_walkingFootStepAudioClips.Length > 0)
                    {
                        int randIndex = Random.Range(0, _walkingFootStepAudioClips.Length - 1);
                        _audioSource.PlayOneShot(_walkingFootStepAudioClips[randIndex], noCrouchVolumeScale);
                        HearingManager.instance.OnSoundEmitted(_audioSource, transform.position, EHeardSoundCategory.EFootstep, 0.8f);
                    }
                    time = 0;
                }
            }
        }
    }
}
