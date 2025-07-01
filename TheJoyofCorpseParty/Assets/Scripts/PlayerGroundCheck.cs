using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField]
    private float m_PlayerHeight;

    [SerializeField]
    private LayerMask m_GroundMask;

    public bool Grounded {  get; private set; }

    // Update is called once per frame
    void Update()
    {
        // Ground check
        Grounded = Physics.Raycast(transform.position, Vector3.down, m_PlayerHeight * 0.5f + 0.2f, m_GroundMask);
    }
}
