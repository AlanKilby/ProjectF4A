using UnityEngine;
using Photon.Pun;
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private Character character;

    [HideInInspector]public float speedMultiplier;

    private PhotonView view;

    public bool canMove;

    private void Start()
    {
        view = transform.GetComponent<PhotonView>();
        speedMultiplier = 1;
        canMove = true;
    }

    private void FixedUpdate()
    {
        if (view.IsMine && canMove)
        {
            rb.velocity = (Input.GetAxisRaw("Vertical") * Vector3.forward+ Input.GetAxisRaw("Horizontal") * Vector3.right).normalized * character.speed * speedMultiplier;
        }
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
}
