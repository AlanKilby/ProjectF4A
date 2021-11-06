using UnityEngine;
using Photon.Pun;
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private Character character;

    private PhotonView view;

    private void Start()
    {
        view = transform.GetComponent<PhotonView>();
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            rb.velocity = (Input.GetAxis("Vertical") * Vector3.forward+ Input.GetAxis("Horizontal") * Vector3.right).normalized * character.speed;
        }
    }
}
