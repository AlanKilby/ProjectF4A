using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FireWalk : MonoBehaviour, ISkills
{
    [SerializeField] private float timeUnderSkillEffect;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private PlayerMovementController playerMovementController;
    [SerializeField] private GameObject damageZone;
    [SerializeField] private Character character;

    public CharacterAnimManager characterAnim;
    public LegAnimManager legAnim;
    public GunAnimation gunAnim; 
    public HorseshoeAnim horseshoeAnim;

    private PhotonView view;



    private bool isActivated;
    private bool isOnCooldown;
    void Start()
    {
        view = transform.GetComponent<PhotonView>();

        isOnCooldown = false;
        isActivated = false;
        damageZone.SetActive(false);
        playerMovementController = GetComponent<PlayerMovementController>();
    }
    public void ActivateSkill()
    {
        view.RPC("ActivateSkillRPC", RpcTarget.All);
        StartCoroutine(CooldownCoroutine());
    }

    public bool IsActivated() {
        
        return this.isActivated;
    }

    public bool IsOnCooldown() {
        return this.isOnCooldown;
    }

    IEnumerator CooldownCoroutine()
    {
        isOnCooldown = true;

        yield return new WaitForSeconds(0.5f);

        if (character.ultimate >= character.ultimateMaxValue)
        {
            isOnCooldown = false;
            character.ultimate = character.ultimateMaxValue;
        }
        else
        {
            character.ultimate += character.ultimateRechargeRate * 0.5f;
            StartCoroutine(CooldownCoroutine());
        }
    }

    [PunRPC]
    public void ActivateSkillRPC() 
    {
        StartCoroutine(ActivateSkillCoroutine(timeUnderSkillEffect));
    }

    IEnumerator ActivateSkillCoroutine(float t)
    {
        isActivated = true;

        legAnim.transform.GetComponent<PhotonView>().RPC("ChangeAnimationState", RpcTarget.All, legAnim.SPECIAL);
        gunAnim.transform.GetComponent<PhotonView>().RPC("ChangeAnimationState", RpcTarget.All, gunAnim.SPECIAL);
        characterAnim.canChangeAnim = false;
        

        // Player movement speed multiplied
        float oldSpeedM = playerMovementController.speedMultiplier;
        playerMovementController.speedMultiplier = speedMultiplier;

        // Damage Zone Activation
        damageZone.SetActive(true);

        yield return new WaitForSeconds(t);



        // Player movement speed back to normal
        playerMovementController.speedMultiplier = oldSpeedM;

        characterAnim.canChangeAnim = true;

        // Damage Zone Deactivation
        damageZone.SetActive(false);

        isActivated = false;
    }
}
