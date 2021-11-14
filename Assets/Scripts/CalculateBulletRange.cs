using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CalculateBulletRange : MonoBehaviour
{
    public Vector3 firstPosition;
    public float range;

    private PhotonView view;

    private Rigidbody rb;

    private void Start()
    {
        transform.GetComponent<Rigidbody>();
        view = transform.GetComponent<PhotonView>();
    }

    private void Update()
    {
        if ((transform.position - firstPosition).magnitude > range) 
        {
            if (view.IsMine) 
            {
                PhotonNetwork.Destroy(this.view);
            }
        }
    }
}
