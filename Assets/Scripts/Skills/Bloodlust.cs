using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bloodlust : MonoBehaviour, ISkills
{
    [SerializeField] private float timeUnderSkillEffect;
    [SerializeField] private float cooldown;
    [SerializeField] private Shoot shootScript;

    private Weapon weapon;
    private int InitialBulletPerShot;

    public CharacterAnimManager characterAnim;
    public LegAnimManager legAnim;
    public GunAnimation gunAnim; 
    public HorseshoeAnim horseshoeAnim;

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

        // Animation
        characterAnim.transform.GetComponent<PhotonView>().RPC("ChangeAnimationState", RpcTarget.All, characterAnim.CHARACTER_SPECIAL);
        gunAnim.transform.GetComponent<PhotonView>().RPC("ChangeGunAnimationState", RpcTarget.All, gunAnim.SPECIAL);
        legAnim.transform.GetComponent<PhotonView>().RPC("ChangeAnimationState", RpcTarget.All, legAnim.SPECIAL);

        // Lock Anim
        characterAnim.canChangeAnim = false;
        gunAnim.canChangeAnim = false;
        legAnim.canChangeAnim = false;

        yield return new WaitForSeconds(t);

        // Unlock Anim
        characterAnim.canChangeAnim = true;
        gunAnim.canChangeAnim = true;
        legAnim.canChangeAnim = true;

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
