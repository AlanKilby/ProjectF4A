using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform player;

    /*private void LateUpdate()
    {
        transform.localPosition = player.position + offset;
    }*/

    public void SetPlayer(Transform player) 
    {
        this.player = player;
        transform.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Follow = player;
        player.GetComponent<Shoot>().SetCamera(transform.GetComponent<Camera>());
    }
}
