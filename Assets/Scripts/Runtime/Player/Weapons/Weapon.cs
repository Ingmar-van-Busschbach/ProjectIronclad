using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private MagazineData magazineData;
    [SerializeField] private TriggerData triggerData;
    [SerializeField] private string barrelEndPointName = "BarrelEndPoint";
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private Rigidbody ship;
    [SerializeField] private Collider[] shipColliders;
    [SerializeField] private LayerMask layerMask;
    
    private PlayerInput controls;
    private int shotInput;
    private Transform barrelEndPointTransform;
    private bool isSelected;
    [HideInInspector] public bool isAimingAtTarget;

    #region Input
    private void Setup()
    {
        controls = new PlayerInput();
        controls.Enable();
        controls.Shoot.Shoot.started += ctx => QueueShot();
        controls.Shoot.Shoot.canceled += ctx => EndShot();
        controls.Shoot.Reload.started += ctx => Reload();
    }
    public void WeaponSelected()
    {
        isSelected = true;
        if(controls == null)
        {
            Setup();
        }
        if (controls.Shoot.Reload.IsPressed())
        {
            QueueShot();
        }
    }

    public void WeaponDeselected()
    {
        isSelected = false;
        shotInput = 0;
    }

    private void OnDisable()
    {
        if(controls != null)
        {
            controls.Disable();
        }
    }

    private void QueueShot()
    {
        shotInput = triggerData.burstCount;
    }

    private void EndShot()
    {
        if (triggerData.automatic)
        {
            shotInput = 0;
        }
    }

    private void Reload()
    {
        magazineData.Reload();
    }
    #endregion

    private void Start()
    {
        magazineData.Start();
        barrelEndPointTransform = transform.Find(barrelEndPointName);
    }

    private void Update()
    {
        magazineData.Update();
        AttemptShot();
    }

    private void AttemptShot()
    {
        if (!isAimingAtTarget || shotInput <= 0 || magazineData.currentFiringDelay > 0 || !isSelected)
        {
            return;
        }
        if (magazineData.CanShoot())
        {
            if (Physics.Raycast(barrelEndPointTransform.position, barrelEndPointTransform.forward, out RaycastHit hit, 100f, layerMask, QueryTriggerInteraction.Ignore))
            {
                foreach(Collider collider in shipColliders)
                {
                    if(hit.collider == collider)
                    {
                        return;
                    }
                }
            }
            if (!triggerData.automatic)
            {
                shotInput--;
            }
            magazineData.Shoot();
            Shoot();
        }
        else if(magazineData.currentMagazine == 0 && magazineData.reloadDelay > 0 && !magazineData.reloadPressed)
        {
            magazineData.Reload();
        }
    }

    private void Shoot()
    {
        Rigidbody newBullet = Instantiate(bulletPrefab, barrelEndPointTransform.position, Quaternion.identity);
        newBullet.linearVelocity = ship.linearVelocity;
        newBullet.AddRelativeForce(transform.forward * 1000);
    }
}