using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerFlashlightAudio : MonoBehaviour
{
    private AudioSource m_AudioSource;

    [SerializeField]
    private AudioClip m_FlashLightAudioClip;

    private PlayerFlashlightHandler _handler;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        _handler = GetComponent<PlayerFlashlightHandler>();
    }

    private void OnEnable()
    {
        _handler.onFlashLight += Handler_OnFlashLight;
    }

    private void Handler_OnFlashLight()
    {
        if(m_FlashLightAudioClip != null)
        {
            m_AudioSource.PlayOneShot(m_FlashLightAudioClip);
            HearingManager.instance.OnSoundEmitted(m_AudioSource, transform.position, EHeardSoundCategory.EFlashlight, 0.3f);
        }
    }

    private void OnDisable()
    {
        _handler.onFlashLight -= Handler_OnFlashLight;
    }
}
