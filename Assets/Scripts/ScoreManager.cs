using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private int scoreMax;

    private Dictionary<GameObject, int> playerScores = new Dictionary<GameObject, int>();
    private GameObject winner;

    [SerializeField] private TMP_Text scoreText;
    private bool scoreTextEnabled;
    private PhotonView view;

    private void Start()
    {
        instance = this;
        scoreTextEnabled = false;
        view = transform.GetComponent<PhotonView>();
    }

    public void AddPlayer(GameObject player)
    {
        playerScores.Add(player, 0);
    }

    private void CheckIfWin(GameObject player)
    {
        if (playerScores[player] >= scoreMax)
        {
            this.winner = player;
            view.RPC("ShowWinner", RpcTarget.All);
        }
    }

    [PunRPC]
    private void ShowWinner()
    {
        scoreTextEnabled = !scoreTextEnabled;
        scoreText.text = "Player wins";
        scoreText.enabled = scoreTextEnabled;
    }

    public void ResetScore()
    {
        foreach (var player in playerScores)
        {
            playerScores[player.Key] = 0;
        }
    }

    public void AddPoint(GameObject player)
    {
        playerScores[player] += 1;
        CheckIfWin(player);
    }

    private void PlayerScoresToString()
    {
        foreach (var player in playerScores)
        {
            Debug.Log(player.Key + " : " + player.Value);
        }
    }
}
