using UnityEngine;

public class DeathPlayerInputDataManager : MonoBehaviour
{
    [SerializeField]
    private NightToShowData nightOneData;

    [SerializeField]
    private NightToShowData nightTwoData;

    public void GoToPlayAgain()
    {
        LoadSceneByNightId();
    }

    private void LoadSceneByNightId()
    {
        string rawData = DataTransferToScene.message;
        Debug.Log(rawData);
        NightDataEnum dataEnum = NightDataEnum.None;
        if (!string.IsNullOrEmpty(rawData))
        {
            object objectEnum;
            if (System.Enum.TryParse(typeof(NightDataEnum), rawData, true, out objectEnum))
            {
                dataEnum = (NightDataEnum)objectEnum;
            }
        }
        switch (dataEnum)
        {
            case NightDataEnum.Night1:
                LevelManager.Instance.LoadScene(nightOneData.sceneBuildId);
                break;
            case NightDataEnum.Night2:
                LevelManager.Instance.LoadScene(nightTwoData.sceneBuildId);
                break;
            default:
                LevelManager.Instance.LoadScene(1);
                break;
        }
    }

    public void GoToMainMenu()
    {
        LevelManager.Instance.LoadScene(0);
    }
}
