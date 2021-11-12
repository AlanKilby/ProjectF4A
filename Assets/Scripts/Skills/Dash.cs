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


    private bool isActivated;
    private bool isOnCooldown;
    void Start()
    {
        isOnCooldown = false;
        isActivated = false;
        playerMovementController = GetComponent<PlayerMovementController>();
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

        float verticalDirection;
        float horizontalDirection;

        // Block player movement and get current direction
        verticalDirection = Input.GetAxisRaw("vertical");
        horizontalDirection = Input.GetAxisRaw("horizontal");
        playerMovementController.canMove = false;

        // Dash Movement
        Rigidbody playerRB = GetComponent<Rigidbody>();
        playerRB.AddForce((Vector3.forward * verticalDirection * dashSpeed) + (Vector3.right * horizontalDirection * dashSpeed), ForceMode.Impulse);

        // Deactivate collider for invulnerability
        healthScript.canBeHit = false;

        yield return new WaitForSeconds(t);

        // Player movement speed back to normal
        playerMovementController.canMove = true;
        healthScript.canBeHit = true;

        isActivated = false;
    }
}
