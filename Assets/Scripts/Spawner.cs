using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    private GameObject player;
    private string playerName;

    [SerializeField] private CameraFollowPlayer cameraFP;
    [SerializeField] private Camera camera;

    [SerializeField] private float minPosX;
    [SerializeField] private float maxPosX;
    [SerializeField] private float minPosZ;
    [SerializeField] private float maxPosZ;
    private Vector3 spawnPos;

    private PhotonView view;

    public void SpawnNewPlayer()
    {
        spawnPos = new Vector3(Random.Range(minPosX, maxPosX), 1.5f, Random.Range(minPosZ, maxPosZ));
        player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
        player.GetComponent<CharacterDisplay>().SetPlayerName(playerName);

        ScoreManager.instance.transform.GetComponent<PhotonView>().RPC("AddPlayer", RpcTarget.All, playerName);

        SetCamera(player);
    }

    private void SetCamera(GameObject player)
    {

        view = player.GetComponent<PhotonView>();

        if (view.IsMine)
        {
            cameraFP.SetPlayer(player.transform);
        }

        player.GetComponent<Shoot>().SetCamera(camera);
    }

    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }
}
