using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour
{

    [SerializeField] private Character character;

    private void Start()
    {
        ChangeMesh();
    }

    private void OnEnable()
    {
        ChangeMesh();
    }

    private void ChangeMesh() 
    {
        transform.GetComponent<MeshFilter>().mesh = character.mesh;
    }

    public Character GetCharacter() 
    {
        return this.character;
    }

}
