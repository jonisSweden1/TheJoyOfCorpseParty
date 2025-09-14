using System;
using UnityEngine;

public class CamInfo : MonoBehaviour
{
    [SerializeField]
    private CamInfoData _data;

    public CamInfoData Data {  get { return _data; } }

    private void Start()
    {
        _data.CamTrans = transform;
    }
}

[Serializable]
public class CamInfoData
{
    [Range(0, 179)]
    public float fieldOfView = 60;
    public Transform CamTrans { get; set; }
}
