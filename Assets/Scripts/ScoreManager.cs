using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private int scoreMax;

    private Dictionary<string, int> playerScores = new Dictionary<string, int>();
    private string winner;

    [SerializeField] private TMP_Text scoreText;
    private bool scoreTextEnabled;
    private PhotonView view;

    private void Start()
    {
        instance = this;
        scoreTextEnabled = false;
        view = transform.GetComponent<PhotonView>();
    }

    [PunRPC]
    public void AddPlayer(string player)
    {
        playerScores.Add(player, 0);
    }

    private void CheckIfWin(string player)
    {
        if (playerScores[player] >= scoreMax)
        {
            this.winner = player;
            view.RPC("ShowWinner", RpcTarget.All, player);
        }
    }

    [PunRPC]
    private void ShowWinner(string winner)
    {
        scoreTextEnabled = !scoreTextEnabled;
        scoreText.text = winner + " wins !";
        scoreText.enabled = scoreTextEnabled;
    }

    public void ResetScore()
    {
        foreach (var player in playerScores)
        {
            playerScores[player.Key] = 0;
        }
    }

    [PunRPC]
    public void AddPoint(string player)
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
