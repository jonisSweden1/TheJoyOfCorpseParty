using UnityEngine;

public class JumpscareManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip _jumpscareAudioClip;

    private AudioSource _jumpscareAudioSource;

    [SerializeField]
    private GameObject _sourceToAppear;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _jumpscareAudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayerDeathManager.m_OnDeath += PlayerDeathManager_m_OnDeath;
    }

    private void PlayerDeathManager_m_OnDeath()
    {
        Play();
    }

    private void Play()
    {
        _jumpscareAudioSource.PlayOneShot(_jumpscareAudioClip, 1.5f);
    }

    private void OnDisable()
    {
        PlayerDeathManager.m_OnDeath -= PlayerDeathManager_m_OnDeath;
    }

    private void Update()
    {
        
    }
}
