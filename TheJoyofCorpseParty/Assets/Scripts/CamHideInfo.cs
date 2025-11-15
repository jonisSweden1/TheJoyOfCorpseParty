using System;
using UnityEngine;

public class CamHideInfo : MonoBehaviour
{
    [SerializeField]
    private CamHideInfoData _data;

    public CamHideInfoData Data {  get { return _data; } }

    private void Start()
    {
        _data.CamTrans = transform;
    }
}

[Serializable]
public class CamHideInfoData
{
    [Range(0, 179)]
    public float fieldOfView = 60;
    public Transform CamTrans { get; set; }
}
