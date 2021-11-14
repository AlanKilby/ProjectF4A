using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public PickUpSpawner spawner;

    public float HPValue;
    public float ultValue;

    public bool isHeal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spawner.SpawnPickUp();

            if (isHeal)
            {
                other.GetComponent<CharacterDisplay>().GetCharacter().hp += HPValue;
            }
            else
            {
                other.GetComponent<CharacterDisplay>().GetCharacter().ultimate += ultValue;
            }

            Destroy(gameObject);
        }
    }

}
