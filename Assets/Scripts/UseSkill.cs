using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkill : MonoBehaviour
{
    private ISkills skill;
    private PhotonView view;

    private void Start()
    {
        skill = transform.GetComponent<ISkills>();
        view = transform.GetComponent<PhotonView>();
    }

    void Update()
    {
        if (view.IsMine) 
        {
            if (Input.GetAxis("Fire3") > 0 && !skill.IsOnCooldown()) 
            {
                skill.ActivateSkill();                
            }
        }
    }
}
