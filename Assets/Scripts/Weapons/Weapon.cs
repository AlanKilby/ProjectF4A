using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;

    public int damage;
    public int magazine;
    public int magazineSizeMax;
    public float reloadTime;
    public int numberBulletReload;
    public float range;
    public float fireRate;
    public int bulletsPerShot;
    public float bulletSpeed;
    public bool isShotgun;
    public float deviation;

    public Mesh mesh;

    public GameObject bulletPrefab;

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
        magazine = magazineSizeMax;
    }

    public void ReloadMagazine() 
    {
        magazine += numberBulletReload;
        if (magazine > magazineSizeMax) magazine = magazineSizeMax;
    }
}
