using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField]private float explosionDuration;
    [SerializeField] private int explosionDamage;
    private SphereCollider explosionCollider;

    private PhotonView view;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        explosionCollider = GetComponent<SphereCollider>();
        StartCoroutine(ExplosionCoroutine(explosionDuration));
    }

    IEnumerator ExplosionCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        explosionCollider.enabled = false;

        yield return new WaitForSeconds(t);
        PhotonNetwork.Destroy(view);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            view = other.transform.GetComponent<PhotonView>();
            if (!view.IsMine)
            {
                other.transform.GetComponent<Health>().TakeDamage(explosionDamage);
                explosionCollider.enabled = false;
            }
        }
    }
}
