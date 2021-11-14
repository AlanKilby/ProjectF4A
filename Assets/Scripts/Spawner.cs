using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public static Spawner instance;

    [SerializeField] private List<GameObject> playerPrefabs;
    private int chosenCharacter;
    private GameObject player;
    private string playerName;

    [SerializeField] private CameraFollowPlayer cameraFP;
    [SerializeField] private Camera camera;

    [SerializeField] private float minPosX;
    [SerializeField] private float maxPosX;
    [SerializeField] private float minPosZ;
    [SerializeField] private float maxPosZ;
    private Vector3 spawnPos;

    [SerializeField] private List<Vector3> possibleSpawns = new List<Vector3>();

    private PhotonView playerView;

    private void Start()
    {
        instance = this;
    }

    private int rand;

    public void SpawnNewPlayer()
    {
        rand = Random.Range(0, possibleSpawns.Count - 1);
        spawnPos = possibleSpawns[rand];
        player = PhotonNetwork.Instantiate(playerPrefabs[chosenCharacter].name, spawnPos, Quaternion.identity);
        player.GetComponent<CharacterDisplay>().SetPlayerName(playerName);
        player.GetComponent<UD_NameDisplay>().DisplayName(playerName);

        gameManager.SetPlayer(player);

        ScoreManager.instance.transform.GetComponent<PhotonView>().RPC("AddPlayer", RpcTarget.All, playerName);

        SetCamera(player);
    }

    private void SetCamera(GameObject player)
    {

        playerView = player.GetComponent<PhotonView>();

        if (playerView.IsMine)
        {
            cameraFP.SetPlayer(player.transform);
        }

        player.GetComponent<Shoot>().SetCamera(camera);
    }

    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }

    public List<Vector3> GetPossibleSpawns() 
    {
        return this.possibleSpawns;
    }

    public void SetChosenCharacter(int chosenCharacter) 
    {
        this.chosenCharacter = chosenCharacter;
    }
}
