using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour
{

    [SerializeField] private Character character;
    private string playerName;

    public float hp;
    public float ultimate;

    private void Start()
    {
        ChangeMesh();
        this.hp = character.hp;
        this.ultimate = character.ultimate;
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

    public void SetPlayerName(string name)
    {
        this.playerName = name;
    }

    public string GetPlayerName()
    {
        return this.playerName;
    }

    public void TakeDamage(int damage)
    {
        this.hp -= damage;
        if (this.hp < 0) this.hp = 0;
    }

    public bool IsDead()
    {
        if (this.hp > 0) return false;
        else return true;
    }

    public void ResetHp()
    {
        this.hp = character.maxHp;
    }

}
