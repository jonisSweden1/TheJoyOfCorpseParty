using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private InputActionReference pauseActionReference;

    private bool isPaused = false;

    private bool isPausingDisabled = false;

    private void OnEnable()
    {
        if (pauseActionReference != null)
        {
            pauseActionReference.action.performed += OnPausePerformed;
            pauseActionReference.action.Enable();
        }

        PlayerDeathManager.m_OnDeath += PlayerDeathManager_m_OnDeath;
    }

    private void PlayerDeathManager_m_OnDeath()
    {
        isPausingDisabled = true;
    }

    private void OnDisable()
    {
        if (pauseActionReference != null)
        {
            pauseActionReference.action.performed -= OnPausePerformed;
            pauseActionReference.action.Disable();
        }

        PlayerDeathManager.m_OnDeath -= PlayerDeathManager_m_OnDeath;
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        if (isPausingDisabled)
        {
            return;
        }

        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        UIManager.instance.GoToMenu(0); // Assuming 0 is the pause menu index

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        AmbienceSoundManager.Instance.PauseMusic();

        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        UIManager.instance.CloseMenu(); // Assuming this closes the current menu

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        AmbienceSoundManager.Instance.ResumeMusic();

        isPaused = false;
    }
}
