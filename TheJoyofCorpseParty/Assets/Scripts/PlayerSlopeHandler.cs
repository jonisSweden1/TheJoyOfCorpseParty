using UnityEngine;

public class PlayerSlopeHandler : MonoBehaviour
{
    public RaycastHit SlopeHit { get { return slopeHit; } }

    private RaycastHit slopeHit;

    [SerializeField]
    private float playerHeight;

    [SerializeField]
    private float maxSlopeAngle;

    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }
}
