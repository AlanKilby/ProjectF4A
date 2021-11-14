using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyAfterXSeconds : MonoBehaviour
{
    public float time;

    private void Start()
    {
        StartCoroutine(DestroyVFX(time));
    }

    IEnumerator DestroyVFX(float t)
    {
        yield return new WaitForSeconds(t);
        PhotonNetwork.Destroy(gameObject);
    }
}
