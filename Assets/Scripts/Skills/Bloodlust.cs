using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodlust : MonoBehaviour, ISkills
{
    [SerializeField] private float timeUnderSkillEffect;
    [SerializeField] private float cooldown;
    [SerializeField] private Shoot shootScript;

    private Weapon weapon;
    private int InitialBulletPerShot;

    private bool isActivated;
    private bool isOnCooldown;

    public void ActivateSkill()
    {
        StartCoroutine(ActivateSkillCoroutine(timeUnderSkillEffect));
        StartCoroutine(CooldownCoroutine(cooldown));
    }

    public bool IsActivated()
    {
        return this.isActivated;
    }

    public bool IsOnCooldown()
    {
        return this.isOnCooldown;
    }

    IEnumerator ActivateSkillCoroutine(float t) 
    {
        isActivated = true;

        weapon.ReloadEntireMagazine();
        weapon.bulletsPerShot = 0;

        shootScript.UpdateUI();

        yield return new WaitForSeconds(t);

        weapon.bulletsPerShot = InitialBulletPerShot;

        isActivated = false;
    }

    IEnumerator CooldownCoroutine(float t) 
    {
        isOnCooldown = true;

        yield return new WaitForSeconds(t);

        isOnCooldown = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        isOnCooldown = false;
        isActivated = false;
        weapon = transform.GetChild(0).GetComponent<WeaponDisplay>().GetWeapon();
        InitialBulletPerShot = weapon.bulletsPerShot;
    }
}
