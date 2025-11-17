using UnityEngine;

[CreateAssetMenu(fileName = "Goals", menuName = "Scriptable Objects/Goals")]
public class Goals : ScriptableObject
{
    public int night;
    public Goal[] goals;
}
