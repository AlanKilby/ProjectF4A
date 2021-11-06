using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDisplay : MonoBehaviour
{

    [SerializeField] private Weapon weapon;

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
        transform.GetComponent<MeshFilter>().mesh = weapon.mesh;
    }

    public Weapon GetWeapon() 
    {
        return this.weapon;
    }
}
