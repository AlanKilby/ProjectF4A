using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LegAnimManager : MonoBehaviour
{
    public string CHARACTER_IDLE;
    public string CHARACTER_WALKING;
    public string SPECIAL;

    [HideInInspector]
    public bool canChangeAnim;

    string currentState;

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
