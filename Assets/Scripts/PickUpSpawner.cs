using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField]
    float respawnTime;

    public GameObject spawnPosition;
    public GameObject pickUp;
    
    public IEnumerator SpawnPickUp()
    {
        GameObject spawnedObject;

        yield return new WaitForSeconds(respawnTime);

        spawnedObject = Instantiate(pickUp, spawnPosition.transform.position, Quaternion.identity);
        spawnPosition.GetComponent<PickUp>().spawner = gameObject.GetComponent<PickUpSpawner>();
    }
}
