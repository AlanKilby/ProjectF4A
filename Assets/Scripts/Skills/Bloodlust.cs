using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bloodlust : MonoBehaviour, ISkills
{
    [SerializeField] private float timeUnderSkillEffect;
    [SerializeField] private Shoot shootScript;
    [SerializeField] private Character character;
    [SerializeField] private CharacterDisplay characterDisplay;

    private Weapon weapon;
    [SerializeField] private WeaponDisplay weaponDisplay;

    private int InitialBulletPerShot;

    public CharacterAnimManager characterAnim;
    public LegAnimManager legAnim;
    public GunAnimation gunAnim; 
    public HorseshoeAnim horseshoeAnim;

    private bool isActivated;
    private bool isOnCooldown;

    public void ActivateSkill()
    {
        characterDisplay.ultimate = 0;
        StartCoroutine(ActivateSkillCoroutine(timeUnderSkillEffect));
        Debug.Log("StartCooldown");
        //StartCoroutine(CooldownCoroutine());
        transform.GetComponent<PhotonView>().RPC("Cooldown", RpcTarget.All);
    }

    public bool IsActivated()
    {
        return this.isActivated;
    }

    public bool IsOnCooldown()
    {
        return this.isOnCooldown;
    }

    [PunRPC]
    public void Cooldown()
    {
        StartCoroutine(CooldownCoroutine());
    }

    IEnumerator ActivateSkillCoroutine(float t) 
    {
        isActivated = true;

        weaponDisplay.ReloadEntireMagazine();
        weaponDisplay.bulletsPerShot = 0;

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

        weaponDisplay.bulletsPerShot = InitialBulletPerShot;

        isActivated = false;
    }

    IEnumerator CooldownCoroutine() 
    {
        isOnCooldown = true;

        yield return new WaitForSeconds(0.5f);

        if (characterDisplay.ultimate >= character.ultimateMaxValue)
        {
            Debug.Log("Cooldown finished");
            isOnCooldown = false;
            characterDisplay.ultimate = character.ultimateMaxValue;
        }
        else
        {
            characterDisplay.ultimate += character.ultimateRechargeRate * 0.5f;
            StartCoroutine(CooldownCoroutine());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isOnCooldown = false;
        isActivated = false;
        weapon = transform.GetChild(0).GetComponent<WeaponDisplay>().GetWeapon();
        InitialBulletPerShot = weaponDisplay.bulletsPerShot;
    }
}
