using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public string characterName;

    public float hp;
    public float maxHp;
    public float speed;
    public float ultimateMaxValue;
    public float ultimate;
    public float ultimateRechargeRate;
    public float ultimateRechargeOnKill;

    public Mesh mesh;

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
        this.hp = this.maxHp;
    }
}
