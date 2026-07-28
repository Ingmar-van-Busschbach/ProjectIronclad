using UnityEngine;

public class TurretRotator : MonoBehaviour
{
    [SerializeField] private TurretAiming turretAiming;
    [SerializeField] private string barrelName = "TurretVertical";
    [SerializeField] private TurretFiringAngles turretFiringAngles;
    private Transform barrelTransform;
    private Transform parent;

    private void Start()
    {
        barrelTransform = transform.Find(barrelName);
        parent = transform.parent;
    }


    private void Update()
    {
        if(barrelTransform == null)
        {
            Debug.Log("Barrel not found!");
            return;
        }
        Vector3 aimRotation = Quaternion.LookRotation(parent.InverseTransformDirection(turretAiming.aimPoint - transform.position)).eulerAngles;
        
        //Clamp horizontal angle
        float horizontalAngle = aimRotation.y;
        horizontalAngle = ((horizontalAngle + 180) % 360) - 180;

        if (turretFiringAngles.horizontalFiringAngles.y - turretFiringAngles.horizontalFiringAngles.x < 360)
        {
            horizontalAngle = Mathf.Clamp(horizontalAngle, turretFiringAngles.horizontalFiringAngles.x, turretFiringAngles.horizontalFiringAngles.y);
        }

        //Clamp vertical angle
        float verticalAngle = aimRotation.x;
        verticalAngle = ((verticalAngle + 180) % 360) - 180;

        Vector2 verticalFiringAngles = turretFiringAngles.GetVerticalAngles(horizontalAngle, true);
        if (verticalFiringAngles.y - verticalFiringAngles.x < 360)
        {
            verticalAngle = Mathf.Clamp(verticalAngle, verticalFiringAngles.x, verticalFiringAngles.y);
        }

        //Apply clamped angles
        Vector3 targetAngles = new Vector3(verticalAngle, horizontalAngle, 0f);
        Vector3 currentAngles = transform.localEulerAngles;
        currentAngles.x = barrelTransform.localEulerAngles.x;

        // If the turret can rotate 360 degrees and is facing backwards, we don't map the intended angles to (-180,180)
        if ((horizontalAngle < -170 || horizontalAngle > 170) && verticalFiringAngles.y - verticalFiringAngles.x >= 360)
        {
            targetAngles = new Vector3(verticalAngle, aimRotation.y, 0);
        }
        // Else we map the current angles to (-180, 180)
        else
        {
            currentAngles = new Vector3(((currentAngles.x + 180) % 360) - 180, ((currentAngles.y + 180) % 360) - 180, 0);
        }
        Vector3 moveAngles = targetAngles - currentAngles;
        moveAngles = new Vector3(Mathf.Clamp(moveAngles.x, -turretFiringAngles.rotationSpeed.y, turretFiringAngles.rotationSpeed.y) * Time.deltaTime, Mathf.Clamp(moveAngles.y, -turretFiringAngles.rotationSpeed.x, turretFiringAngles.rotationSpeed.x) * Time.deltaTime, 0);
        Quaternion horiztonalRotation = Quaternion.Euler(new Vector3(0, currentAngles.y, 0) + new Vector3(0, moveAngles.y, 0));
        transform.localRotation = horiztonalRotation;
        Quaternion verticalRotation = Quaternion.Euler(new Vector3(currentAngles.x, 0, 0) + new Vector3(moveAngles.x, 0, 0));
        barrelTransform.localRotation = verticalRotation;
    }
}
