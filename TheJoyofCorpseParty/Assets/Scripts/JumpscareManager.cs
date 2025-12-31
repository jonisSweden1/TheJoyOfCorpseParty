using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class JumpscareManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _sourceToAppear;

    private PlayableDirector _jumpscareTimeline;

    private bool _hasCatchedPlayer = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(_sourceToAppear != null)
        {
            _jumpscareTimeline = _sourceToAppear.GetComponent<PlayableDirector>();
        }
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
        _sourceToAppear.SetActive(true);
        _jumpscareTimeline.Play();
        _hasCatchedPlayer = true;
    }

    private void OnDisable()
    {
        PlayerDeathManager.m_OnDeath -= PlayerDeathManager_m_OnDeath;
    }

    private void Update()
    {
        if(_hasCatchedPlayer)
        {
            if (_jumpscareTimeline.state != PlayState.Playing)
            {
                _sourceToAppear.SetActive(false);
            }
        }
    }
}
