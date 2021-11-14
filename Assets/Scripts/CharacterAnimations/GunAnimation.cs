using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    [PunRPC]
    public void ChangeGunAnimationState(string newState)
    {
        Debug.Log("ChangeGunAnimationState entered !");
        if (canChangeAnim)
        {
            if (currentState == newState) return;

            characterAnimator.Play(newState);

            currentState = newState;
        }
    }
}
