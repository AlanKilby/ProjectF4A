using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UD_CameliaBulletCounter : MonoBehaviour
{
    public GameObject[] ammoImage;
    public Weapon weapon;
    public WeaponDisplay weaponDisplay;

    private PhotonView view;

    private void Start()
    {
        view = transform.GetComponent<PhotonView>();
    }

    void Update()
    {
        if (view.IsMine)
        {
            for (int i = 0; i < weapon.magazineSizeMax; i++)
            {
                if (i >= weaponDisplay.magazine)
                {
                    ammoImage[i].SetActive(false);
                }
                else
                {
                    ammoImage[i].SetActive(true);
                }
            }
        }
    }
}
