using UnityEngine;
using UnityEngine.Events;

public class ExitManager : MonoBehaviour
{
    public static ExitManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void CloseApplication_ShowPopUpMessage()
    {
        UnityAction action = new UnityAction(CloseApplication);
        UIManager.instance.ShowPopUp("Are you sure you want to leave?", action);
    }

    public void CloseApplication()
    {
        if (Application.isPlaying)
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
