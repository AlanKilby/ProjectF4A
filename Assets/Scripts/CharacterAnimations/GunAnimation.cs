using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    public string IDLE;
    public string FIRE;
    public string SPECIAL;

    string currentState;

    [HideInInspector]
    public bool canChangeAnim;

    public PlayerMovementController playerMovementController;
    public Animator characterAnimator;
    private void Start()
    {
        canChangeAnim = true;
    }

    public void ChangeGunAnimationState(string newState)
    {
        if (canChangeAnim)
        {
            if (currentState == newState) return;

            characterAnimator.Play(newState);

            currentState = newState;
        }
    }
}
