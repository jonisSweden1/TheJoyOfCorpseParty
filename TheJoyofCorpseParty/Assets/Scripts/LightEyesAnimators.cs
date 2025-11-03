using UnityEngine;

public class LightEyesAnimators : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void TurnOnLightsStart()
    {
        _animator.Play("EyesLightOn", 0);
    }

    public void TurnOnLights()
    {
        _animator.SetTrigger("Awake");
    }
}
