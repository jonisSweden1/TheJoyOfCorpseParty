using UnityEngine;

[CreateAssetMenu(fileName = "NewNightToShowData", menuName = "Night To Show/Data", order = 1)]
public class NightToShowData : ScriptableObject
{
    public string title;
    public string description;
    public int sceneBuildId;
}