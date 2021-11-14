using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class AudioManager : MonoBehaviour
{
    /* Clip Numbers
     * 1 shoot
     * 2 special
     * 3 mort
    */
    public AudioClip[] characterClips;


    public AudioSource audioSource;

    [PunRPC]
    public void PlayAudioClip(int clipNumber)
    {
        audioSource.PlayOneShot(characterClips[clipNumber-1]);
    }

    //audioManager.GetComponent<PhotonView>().RPC("PlayAudioClip", RpcTarget.All, 1);
}
