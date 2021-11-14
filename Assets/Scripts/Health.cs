using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviour
{
    [SerializeField] private CharacterDisplay character;

    private GameObject damagedBy;

    [SerializeField] private List<Vector3> possibleSpawns;

    private PhotonView view;

    private MeshRenderer meshRenderer;

    public bool canBeHit;

    public GameObject deathExplosion;

    private void Start()
    {
        view = transform.GetComponent<PhotonView>();
        meshRenderer = transform.GetComponent<MeshRenderer>();
        possibleSpawns = Spawner.instance.GetPossibleSpawns();
        canBeHit = true;
    }

    public void TakeDamage(int damage) 
    {
        if (canBeHit)
        {
            view.RPC("TakeDamage", RpcTarget.All, damage);
            //view.RPC("UpdateHealthBar", RpcTarget.All);
            CheckIsDead();
        }
        
    }

    private Character damagedByCharacter;

    private void CheckIsDead() 
    {
        if (character.IsDead()) 
        {
            PhotonNetwork.Instantiate(deathExplosion.name, transform.position, Quaternion.identity);
            view.RPC("TeleportPlayer", RpcTarget.All);
            damagedByCharacter = damagedBy.GetComponent<CharacterDisplay>().GetCharacter();
            ScoreManager.instance.transform.GetComponent<PhotonView>().RPC("AddPoint", RpcTarget.All, damagedBy.GetComponent<CharacterDisplay>().GetPlayerName());

            damagedByCharacter.ResetHp();
            damagedByCharacter.ultimate += damagedByCharacter.ultimateRechargeOnKill;
        } 
    }

    [PunRPC]
    private void TeleportPlayer() 
    {
        StartCoroutine(TeleportPlayerCoroutine());
    }

    IEnumerator TeleportPlayerCoroutine() 
    {
        //view.RPC("ResetHp", RpcTarget.All);
        character.ResetHp();
        meshRenderer.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false); // l'arme du joueur

        transform.position = SelectRandomSpawn();

        yield return new WaitForSeconds(1);

        //view.RPC("ResetHP", RpcTarget.All);
        character.ResetHp();
        transform.GetChild(0).gameObject.SetActive(true);
        meshRenderer.enabled = true;
    }

    private int rand;

    private Vector3 SelectRandomSpawn() 
    {
        rand = Random.Range(0, possibleSpawns.Count - 1);
        return possibleSpawns[rand];
    }

    public void SetDamagedBy(GameObject damagedBy)
    {
        this.damagedBy = damagedBy;
    }
}
