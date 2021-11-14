using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private int damage;

    private GameObject player;
    private Health damagedPlayerHealth;

    private PhotonView view;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            view = other.transform.GetComponent<PhotonView>();
            if (!view.IsMine)
            {
                damagedPlayerHealth = other.transform.GetComponent<Health>();
                damagedPlayerHealth.SetDamagedBy(player);
                damagedPlayerHealth.TakeDamage(damage);
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

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
