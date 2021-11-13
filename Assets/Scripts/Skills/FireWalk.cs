using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWalk : MonoBehaviour, ISkills
{
    [SerializeField] private float cooldown;
    [SerializeField] private float timeUnderSkillEffect;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private PlayerMovementController playerMovementController;
    [SerializeField] private GameObject damageZone;


    private bool isActivated;
    private bool isOnCooldown;
    void Start()
    {
        isOnCooldown = false;
        isActivated = false;
        damageZone.SetActive(false);
        playerMovementController = GetComponent<PlayerMovementController>();
    }
    public void ActivateSkill()
    {
        StartCoroutine(ActivateSkillCoroutine(timeUnderSkillEffect));
        StartCoroutine(CooldownCoroutine(cooldown));
    }

    public bool IsActivated() {
        
        return this.isActivated;
    }

    public bool IsOnCooldown() {
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

        // Player movement speed multiplied
        float oldSpeedM = playerMovementController.speedMultiplier;
        playerMovementController.speedMultiplier = speedMultiplier;

        // Damage Zone Activation
        damageZone.SetActive(true);

        yield return new WaitForSeconds(t);

        // Player movement speed back to normal
        playerMovementController.speedMultiplier = oldSpeedM;

        // Damage Zone Deactivation
        damageZone.SetActive(false);

        isActivated = false;
    }
}
