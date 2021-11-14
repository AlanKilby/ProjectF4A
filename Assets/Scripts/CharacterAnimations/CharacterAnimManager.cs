using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterAnimManager : MonoBehaviour
{
    public string CHARACTER_IDLE;
    public string CHARACTER_WALKING;
    public string CHARACTER_DEATH;
    public string CHARACTER_SPECIAL;

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
    public void ChangeAnimationState(string newState)
    {
        if (canChangeAnim)
        {
            if (currentState == newState) return;

            characterAnimator.Play(newState);

            currentState = newState;
        }
    }

    
}
