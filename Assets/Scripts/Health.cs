using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviourPunCallbacks
{
    [SerializeField] private Character character;

    private GameObject damagedBy;

    private Vector3 spawnPos = new Vector3(0, 1.5f, 0);
    [SerializeField] private List<Vector3> possibleSpawns = new List<Vector3>();

    private PhotonView view;

    private MeshRenderer meshRenderer;


    private void Start()
    {
        view = transform.GetComponent<PhotonView>();
        meshRenderer = transform.GetComponent<MeshRenderer>();

        possibleSpawns.Add(spawnPos);
    }

    public void TakeDamage(int damage) 
    {
        character.TakeDamage(damage);
        CheckIsDead();
    }

    private void CheckIsDead() 
    {
        if (character.IsDead()) 
        {
            Debug.Log("Died !");
            view.RPC("TeleportPlayer", RpcTarget.All);
            ScoreManager.instance.AddPoint(damagedBy.GetComponent<CharacterDisplay>().GetPlayerName());
        } 
    }

    [PunRPC]
    private void TeleportPlayer() 
    {
        StartCoroutine(TeleportPlayerCoroutine());
    }

    IEnumerator TeleportPlayerCoroutine() 
    {
        meshRenderer.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false); // l'arme du joueur

        transform.position = SelectRandomSpawn();

        yield return new WaitForSeconds(1);

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
