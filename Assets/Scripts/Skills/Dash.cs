using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour, ISkills
{
    [SerializeField] private float cooldown;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] Health healthScript;
    [SerializeField] private PlayerMovementController playerMovementController;

    public CharacterAnimManager characterAnim;
    public LegAnimManager legAnim;
    public GunAnimation gunAnim;

    float verticalDirection;
    float horizontalDirection;

    private bool isActivated;
    private bool isOnCooldown;
    void Start()
    {
        isOnCooldown = false;
        isActivated = false;
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    private void Update()
    {
        if(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            verticalDirection = Input.GetAxisRaw("Vertical");
            horizontalDirection = Input.GetAxisRaw("Horizontal");            
    }
    public void ActivateSkill()
    {
        StartCoroutine(ActivateSkillCoroutine(dashTime));
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

    IEnumerator CooldownCoroutine(float t)
    {
        isOnCooldown = true;

        yield return new WaitForSeconds(t);

        isOnCooldown = false;
    }

    IEnumerator ActivateSkillCoroutine(float t)
    {
        isActivated = true;

        // Animation
        characterAnim.ChangeAnimationState(characterAnim.CHARACTER_SPECIAL);
        gunAnim.ChangeGunAnimationState(gunAnim.SPECIAL);
        legAnim.ChangeAnimationState(legAnim.SPECIAL);

        // Lock Anim
        characterAnim.canChangeAnim = false;
        gunAnim.canChangeAnim = false;
        legAnim.canChangeAnim = false;

        // Block player movement and get current direction
        
        playerMovementController.SetCanMove(false);

        // Dash Movement
        Rigidbody playerRB = GetComponent<Rigidbody>();
        playerRB.AddForce((Vector3.forward * verticalDirection * dashSpeed) + (Vector3.right * horizontalDirection * dashSpeed), ForceMode.VelocityChange);

        // Deactivate collider for invulnerability
        healthScript.canBeHit = false;

        yield return new WaitForSeconds(t);

        // Unlock Anim
        characterAnim.canChangeAnim = true;
        gunAnim.canChangeAnim = true;
        legAnim.canChangeAnim = true;

        // Player movement speed back to normal
        playerRB.velocity = Vector3.zero;
        playerMovementController.canMove = true;
        healthScript.canBeHit = true;

        isActivated = false;
    }
}
