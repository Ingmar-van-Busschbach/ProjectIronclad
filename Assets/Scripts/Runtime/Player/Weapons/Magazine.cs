using UnityEngine;

[CreateAssetMenu(fileName = "BulletMagazine", menuName = "ScriptableObjects/Player/BulletMagazine", order = 1)]
public class Magazine : ScriptableObject
{
    [HideInInspector] public float currentMagazine;
    public float maxMagazine;
    public virtual bool CanShoot()
    {
        return currentMagazine > 0;
    }

    public virtual void Reload()
    {
        currentMagazine = maxMagazine;
    }

    public virtual void Shoot()
    {
        currentMagazine--;
    }

    public virtual void Start()
    {
        Reload();
    }

    public virtual void Update()
    {

    }
}
