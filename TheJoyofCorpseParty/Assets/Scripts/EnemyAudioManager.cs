using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private EnemyStateManager _enemyStateManager;

    [SerializeField]
    private AudioClip[] _walkingFootStepsAudioClips, _runningFootStepsAudioClips;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _enemyStateManager = GetComponent<EnemyStateManager>();
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
