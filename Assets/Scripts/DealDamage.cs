using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DealDamage : MonoBehaviour
{
    private int damage;

    private PhotonView view;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            view = other.transform.GetComponent<PhotonView>();
            if (!view.IsMine)
            {
                other.transform.GetComponent<Health>().TakeDamage(damage);
            }
        }

        view = transform.GetComponent<PhotonView>();
        if (view.IsMine) 
        {
            PhotonNetwork.Destroy(view);
        }
    }

    public void SetDamage(int damage) 
    {
        this.damage = damage;
    }

    public int GetDamage() 
    {
        return this.damage;
    }
}
