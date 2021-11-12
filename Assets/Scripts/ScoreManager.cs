using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//
//  SI VOUS AVEZ BESOIN DE MODIFIER AUTRE CHOSE QUE LES VARIABLES SERIALISÉES DANS CETTE CLASSE, PREVENEZ MOI
//

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private GameManager gameManager;
    private PhotonView gameManagerView;
    
    [SerializeField] private int scoreMax;

    private Dictionary<string, int> playerScores = new Dictionary<string, int>();
    private string winner;

    [SerializeField] private TMP_Text scoreText;
    private PhotonView view;

    private void Start()
    {
        instance = this;
        view = transform.GetComponent<PhotonView>();
        gameManagerView = gameManager.transform.GetComponent<PhotonView>();
        view.RPC("UpdateDictionary", RpcTarget.All);
    }

    [PunRPC]
    public void AddPlayer(string player)
    {
        if (!playerScores.ContainsKey(player)) 
        {
            playerScores.Add(player, 0);
        }
        PlayerScoresToString();
    }

    [PunRPC]
    public void UpdateDictionary() 
    {
        foreach (var player in playerScores)
        {
            view.RPC("AddPlayer", RpcTarget.All, player.Key);
        }
    }

    private void CheckIfWin(string player)
    {
        if (playerScores[player] >= scoreMax)
        {
            this.winner = player;
            view.RPC("ShowWinner", RpcTarget.All, player);
            gameManagerView.RPC("RestartGame", RpcTarget.All);
            view.RPC("ResetScore", RpcTarget.All);
        }
    }

    [PunRPC]
    private void ShowWinner(string winner)
    {
        scoreText.text = winner + " wins !";
        scoreText.enabled = !scoreText.enabled;
    }

    Dictionary<string, int> playerScoresBuffer;

    [PunRPC]
    public void ResetScore()
    {
        playerScoresBuffer = new Dictionary<string, int>();
        foreach (var player in playerScores)
        {
            playerScoresBuffer.Add(player.Key, 0);
        }
        playerScores = playerScoresBuffer;
        PlayerScoresToString();
    }

    [PunRPC]
    public void AddPoint(string player)
    {
        playerScores[player] += 1;
        PlayerScoresToString();
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
