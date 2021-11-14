using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseshoeAnim : MonoBehaviour
{
    public string SPINNING;
    public string INVISIBLE;

    [HideInInspector]
    public bool canChangeAnim;

    string currentState;

    public PlayerMovementController playerMovementController;
    public Animator characterAnimator;
    private void Start()
    {
        canChangeAnim = true;
    }
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
