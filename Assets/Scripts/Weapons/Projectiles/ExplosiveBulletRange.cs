using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class ExplosiveBulletRange : MonoBehaviour
{
    public Vector3 firstPosition;
    public float range;

    public GameObject explosion;

    private PhotonView view;

    private void Start()
    {
        view = transform.GetComponent<PhotonView>();
    }

    private void Update()
    {
        if ((transform.position - firstPosition).magnitude > range)
        {
            if (view.IsMine)
            {
                PhotonNetwork.Instantiate(explosion.name, transform.position, Quaternion.identity);
                PhotonNetwork.Destroy(view);
            }
        }
    }
}
