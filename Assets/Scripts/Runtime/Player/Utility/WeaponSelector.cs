using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    public List<Weapon> weaponGroupA = new();
    public List<Weapon> weaponGroupB = new();

    private PlayerInput controls;
    private bool selectGroupA;

    private void Awake()
    {
        controls = new PlayerInput();

        controls.Shoot.SelectWeapon.started += ctx => SelectWeapon();
        foreach (Weapon weapon in weaponGroupA)
        {
            weapon.WeaponSelected();
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void SelectWeapon()
    {
        if (selectGroupA)
        {
            Debug.Log("Selecting Weapon Group A");
            selectGroupA = false;
            foreach(Weapon weapon in weaponGroupA)
            {
                weapon.WeaponSelected();
            }
            foreach (Weapon weapon in weaponGroupB)
            {
                weapon.WeaponDeselected();
            }
        }
        else
        {
            Debug.Log("Selecting Weapon Group B");
            selectGroupA = true;
            foreach (Weapon weapon in weaponGroupB)
            {
                weapon.WeaponSelected();
            }
            foreach (Weapon weapon in weaponGroupA)
            {
                weapon.WeaponDeselected();
            }
        }
    }
}
