using UnityEngine;
using UnityEngine.Events;

public class ExitManager : MonoBehaviour
{
    public void CloseApplication_ShowPopUpMessage()
    {
        UnityAction action = new UnityAction( () =>
        {
            if (Application.isPlaying)
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        });
        UIManager.instance.ShowPopUp("Are you sure you want to leave?", action);
    }
}
