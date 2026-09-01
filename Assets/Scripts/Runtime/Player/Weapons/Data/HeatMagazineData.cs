using UnityEngine;

[CreateAssetMenu(fileName = "HeatMagazine", menuName = "ScriptableObjects/Weapons/HeatMagazine", order = 1)]
public class HeatMagazineData : MagazineData
{
    public float coolSpeed;
    public float reloadCoolSpeed;
    public override bool CanShoot()
    {
        return currentMagazine < maxMagazine - 1 && !reloadPressed;
    }

    public override void Reload()
    {
        reloadPressed = true;
    }

    public override void Shoot()
    {
        currentMagazine++;
        currentFiringDelay = firingDelay;
    }

    public override void Start()
    {
        Reload();
    }

    public override void Update()
    {
        
        if (reloadPressed)
        {
            currentMagazine -= reloadCoolSpeed * Time.deltaTime;
        }
        else
        {
            currentMagazine -= coolSpeed * Time.deltaTime;
        }
        currentMagazine = Mathf.Clamp(currentMagazine, 0, maxMagazine);
        if (currentMagazine == 0)
        {
            reloadPressed = false;
        }
        currentFiringDelay -= Time.deltaTime;
        currentFiringDelay = Mathf.Clamp(currentFiringDelay, 0, firingDelay);
    }
}
