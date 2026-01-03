using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private InputActionReference pauseActionReference;

    private bool isPaused = false;

    private void OnEnable()
    {
        if (pauseActionReference != null)
        {
            pauseActionReference.action.performed += OnPausePerformed;
            pauseActionReference.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (pauseActionReference != null)
        {
            pauseActionReference.action.performed -= OnPausePerformed;
            pauseActionReference.action.Disable();
        }
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
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

        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        UIManager.instance.CloseMenu(); // Assuming this closes the current menu

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isPaused = false;
    }
}
