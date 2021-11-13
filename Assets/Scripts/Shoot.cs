using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Shoot : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform weaponTransform;
    private Weapon weapon;

    private GameObject bullet;
    private Transform bulletGroup;
    private Rigidbody rb;
    private CalculateBulletRange bulletRange;
    private DealDamage bulletDamage;

    [SerializeField] private Camera camera;

    private Vector3 mouseScreenPoint;
    private Vector3 mouseWorldPosition;
    private Vector3 shotDirection;

    private bool canFire;
    private bool isReloading;

    [SerializeField] private TMP_Text magazineUI;

    private PhotonView view;

    private void Start()
    {
        view = transform.GetComponent<PhotonView>();

        weapon = weaponTransform.GetComponent<WeaponDisplay>().GetWeapon();
        weapon.ReloadEntireMagazine();

        canFire = true;
        isReloading = false;

        UpdateUI();
    }

    void Update()
    {
        if (view.IsMine)
        {

            CalculateMouseWorldPosition();
            if (Input.GetAxis("Fire1") > 0 && canFire && weapon.HasAmmo())
            {
                Fire();
            }
            else if ((Input.GetAxis("Fire2") > 0 || !weapon.HasAmmo()) && !isReloading)
            {
                Reload();
            }

            LookAt();

        }
    }

    public void UpdateUI() 
    {
        if(view.IsMine)
            magazineUI.text = weapon.name + " : " + weapon.magazine + " / " + weapon.magazineSizeMax;
    }

    private void Fire()
    {
        weapon.UpdateMagazine();
        UpdateUI();

        StartCoroutine(CooldownFireRateCoroutine(weapon.fireRate));

        findWeaponType();
    }

    IEnumerator CooldownFireRateCoroutine(float t)
    {
        canFire = false;

        yield return new WaitForSeconds(t);

        canFire = true;
    }

    private float currentDeviation;

    private void findWeaponType() 
    {
        if (weapon.isShotgun)
        {
            currentDeviation = -weapon.deviation;
            for (int i = 0; i < 3; i++)
            {
                Debug.Log("shotgun entered");
                CreateBullet(currentDeviation);
                currentDeviation += weapon.deviation;
            }
        }
        else
        {
            CreateBullet(0);
        }
    }

    private void CreateBullet(float deviation)
    {
        CalculateShotDirection(deviation);

        bullet = Pooler.instance.Pop("Bullet");
        bullet.transform.position = weaponTransform.position + weaponTransform.forward + weaponTransform.right * deviation * 0.02f;
        rb = bullet.GetComponent<Rigidbody>();

        bulletRange = bullet.GetComponent<CalculateBulletRange>();
        bulletRange.firstPosition = bullet.transform.position;
        bulletRange.range = weapon.range;

        bulletDamage = bullet.GetComponent<DealDamage>();
        bulletDamage.SetDamage(weapon.damage);
        bulletDamage.SetPlayer(transform.gameObject);

        rb.AddForce(shotDirection * weapon.bulletSpeed, ForceMode.Impulse);
    }

    private void Reload() 
    {
        StopAllCoroutines();
        StartCoroutine(ReloadCoroutine(weapon.reloadTime));
    }

    IEnumerator ReloadCoroutine(float t) 
    {
        canFire = false;
        isReloading = true;

        yield return new WaitForSeconds(t);

        canFire = true;
        isReloading = false;
        weapon.ReloadEntireMagazine();
        UpdateUI();
    }

    private void CalculateShotDirection(float deviation) 
    {
        shotDirection = Quaternion.Euler(0, deviation, 0) * (mouseWorldPosition - weaponTransform.TransformPoint(Vector3.forward)).normalized;
    }

    private Ray ray;
    private RaycastHit hit;

    private void CalculateMouseWorldPosition() 
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) 
        {
            mouseWorldPosition = hit.point - Vector3.down;
        }
    }

    private void LookAt() 
    {
        transform.LookAt(mouseWorldPosition);
    }

    public void SetCamera(Camera camera) 
    {
        this.camera = camera;
    }

    public void SetCanFire(bool canFire)
    {
        this.canFire = canFire;
    }
}
