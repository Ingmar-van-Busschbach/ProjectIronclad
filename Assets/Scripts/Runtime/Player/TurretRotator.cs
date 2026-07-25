using UnityEngine;

public class TurretRotator : MonoBehaviour
{
    [SerializeField] private TurretAiming turretAiming;
    [SerializeField] private string barrelName = "TurretVertical";
    private Transform barrelTransform;
    private Transform parent;

    private void Start()
    {
        barrelTransform = transform.Find(barrelName);
        parent = transform.parent.transform;
    }


    private void Update()
    {
        if(barrelTransform == null)
        {
            Debug.Log("Barrel not found!");
            return;
        }
        Vector3 aimRotation = Quaternion.LookRotation(parent.InverseTransformDirection(turretAiming.aimPoint - transform.position)).eulerAngles;
        transform.localRotation = Quaternion.Euler(0, aimRotation.y, 0);
        barrelTransform.localRotation = Quaternion.Euler(aimRotation.x, 0, 0);
    }
}
