using UnityEngine;

[CreateAssetMenu(fileName = "HeatMagazine", menuName = "ScriptableObjects/Player/HeatMagazine", order = 1)]
public class HeatMagazine : Magazine
{
    public float CoolSpeed;
    public override bool CanShoot()
    {
        return currentMagazine < maxMagazine - 1;
    }

    public override void Reload()
    {
        currentMagazine = 0;
    }

    public override void Shoot()
    {
        currentMagazine++;
    }

    public override void Start()
    {
        Reload();
    }

    public override void Update()
    {
        currentMagazine -= CoolSpeed * Time.deltaTime;
        currentMagazine = Mathf.Clamp(currentMagazine, 0, maxMagazine);
    }
}
