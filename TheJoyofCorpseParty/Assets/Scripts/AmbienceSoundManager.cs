using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbienceSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _ambienceClips;

    private AudioSource _source;

    void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    void Start()
    {
        if(_ambienceClips != null)
        {
            int random = Random.Range(0, _ambienceClips.Length - 1);
            _source.clip = _ambienceClips[random];
            _source.Play();
            _source.loop = true;
        }
    }
}
