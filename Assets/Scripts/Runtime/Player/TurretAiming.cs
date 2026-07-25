using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
public class TurretAiming : MonoBehaviour
{
    public Vector3 aimPoint;
    [SerializeField] private float aimRange = 10000f;
    [SerializeField] private LayerMask layerMask;
    private CinemachineCamera cam;

    private void Start()
    {
        cam = GetComponent<CinemachineCamera>();
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, aimRange, layerMask, QueryTriggerInteraction.Ignore))
        {
            aimPoint = hit.point;
        }
        else
        {
            aimPoint = transform.position + transform.forward * aimRange;
        }
    }
}
