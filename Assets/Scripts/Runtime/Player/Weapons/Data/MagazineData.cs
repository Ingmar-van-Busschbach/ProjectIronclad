using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletMagazine", menuName = "ScriptableObjects/Weapons/BulletMagazine", order = 1)]
public class MagazineData : ScriptableObject
{
    public float maxMagazine;
    public float firingDelay;
    [Tooltip("Keep at 0 on a heat magazine")] public float reloadDelay;
    [HideInInspector] public float currentMagazine;
    [HideInInspector] public float currentFiringDelay;
    [HideInInspector] public float currentReloadDelay;
    [HideInInspector] public bool reloadPressed;
    public virtual bool CanShoot()
    {
        return currentMagazine > 0 && !reloadPressed;
    }

    public virtual void Reload()
    {
        reloadPressed = true;
        currentReloadDelay = reloadDelay;
    }

    public virtual void Shoot()
    {
        currentMagazine--;
        currentFiringDelay = firingDelay;
    }

    public virtual void Start()
    {
        Reload();
    }

    public virtual void Update()
    {
        currentFiringDelay -= Time.deltaTime;
        currentFiringDelay = Mathf.Clamp(currentFiringDelay, 0, firingDelay);
        if (reloadPressed)
        {
            currentReloadDelay -= Time.deltaTime;
            if(currentReloadDelay <= 0)
            {
                currentMagazine = maxMagazine;
                reloadPressed = false;
            }
        }
    }
}
