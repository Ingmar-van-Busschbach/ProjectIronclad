using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
public class TurretAiming : MonoBehaviour
{
    public Vector3 aimPoint;
    [SerializeField] private float aimRange = 10000f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Collider[] objectsToIgnore;
    private CinemachineCamera cam;

    private void Start()
    {
        cam = GetComponent<CinemachineCamera>();
    }

    private void Update()
    {
        bool foundObject = false;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, aimRange, layerMask, QueryTriggerInteraction.Ignore);
        if (hits.Length > 0)
        {
            foreach(RaycastHit hit in hits)
            {
                bool shouldIgnore = false;
                foreach (Collider objectToIgnore in objectsToIgnore)
                {
                    if(hit.collider == objectToIgnore)
                    {
                        shouldIgnore = true;
                        break;
                    }
                }
                if (!shouldIgnore)
                {
                    foundObject = true;
                    aimPoint = hit.point;
                    break;
                }
            }
        }
        if (!foundObject)
        {
            aimPoint = transform.position + transform.forward * aimRange;
        }
    }
}
