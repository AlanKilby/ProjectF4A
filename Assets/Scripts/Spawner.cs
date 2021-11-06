using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    private GameObject player;

    [SerializeField] private CameraFollowPlayer cameraFP;
    [SerializeField] private Camera camera;

    [SerializeField] private float minPosX;
    [SerializeField] private float maxPosX;
    [SerializeField] private float minPosZ;
    [SerializeField] private float maxPosZ;
    private Vector3 spawnPos;

    private PhotonView view;

    private void Start()
    {
        spawnPos = new Vector3(Random.Range(minPosX, maxPosX), 1.5f, Random.Range(minPosZ, maxPosZ));
        player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);

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
}
