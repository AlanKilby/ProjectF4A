using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDisplay : MonoBehaviour
{

    [SerializeField] private Weapon weapon;

    public int magazine;
    public int bulletsPerShot;

    private void Start()
    {
        ChangeMesh();
        this.magazine = weapon.magazine;
        this.bulletsPerShot = weapon.bulletsPerShot;
    }

    private void OnEnable()
    {
        ChangeMesh();
    }

    private void ChangeMesh()
    {
        transform.GetComponent<MeshFilter>().mesh = weapon.mesh;
    }

    public Weapon GetWeapon() 
    {
        return this.weapon;
    }

    public bool HasAmmo()
    {
        if (magazine > 0) return true;
        else return false;
    }

    public void UpdateMagazine()
    {
        magazine -= bulletsPerShot;

        if (magazine <= 0) magazine = 0;
    }

    public void ReloadEntireMagazine()
    {
        magazine = weapon.magazineSizeMax;
    }

    public void ReloadMagazine()
    {
        magazine += weapon.numberBulletReload;
        if (magazine > weapon.magazineSizeMax) magazine = weapon.magazineSizeMax;
    }
}
