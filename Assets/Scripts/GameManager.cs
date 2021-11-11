using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private GameObject player;
    private PlayerMovementController playerMovementController;
    private Shoot playerShoot;

    [SerializeField] private float timeBeforeRestart;

    [SerializeField] private TMP_Text restartTimerText;
    [SerializeField] private TMP_Text winnerText;

    [PunRPC]
    public void RestartGame()
    {
        playerMovementController = player.GetComponent<PlayerMovementController>();
        playerShoot = player.GetComponent<Shoot>();

        restartTimerText.enabled = true;

        StartCoroutine(UpdateRestartTimerCoroutine(timeBeforeRestart));
        StartCoroutine(RestartGameCoroutine(timeBeforeRestart));
    }

    IEnumerator RestartGameCoroutine(float t)
    {
        playerMovementController.SetCanMove(false);
        playerShoot.SetCanFire(false);

        yield return new WaitForSeconds(t);

        playerMovementController.SetCanMove(true);
        playerShoot.SetCanFire(true);
    }

    IEnumerator UpdateRestartTimerCoroutine(float t)
    {
        Debug.Log("Update restart timer coroutine entered");
        restartTimerText.text = "Restarts in " + t + " seconds";

        yield return new WaitForSeconds(1);

        if (t > 0)
        {
            StartCoroutine(UpdateRestartTimerCoroutine(t - 1));
        }
        else
        {
            winnerText.enabled = false;
            restartTimerText.enabled = false;
        }
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
