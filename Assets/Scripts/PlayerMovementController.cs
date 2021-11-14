using UnityEngine;
using Photon.Pun;
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private Character character;

    [SerializeField] private CharacterAnimManager characterAnim;
    [SerializeField] private LegAnimManager legAnim;

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
            rb.velocity = (Input.GetAxisRaw("Vertical") * Vector3.forward + Input.GetAxisRaw("Horizontal") * Vector3.right).normalized * character.speed * speedMultiplier;

            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                characterAnim.transform.GetComponent<PhotonView>().RPC("ChangeAnimationState", RpcTarget.All, characterAnim.CHARACTER_WALKING);
                legAnim.transform.GetComponent<PhotonView>().RPC("ChangeAnimationState", RpcTarget.All, legAnim.CHARACTER_WALKING);
            }
            else 
            {
                characterAnim.transform.GetComponent<PhotonView>().RPC("ChangeAnimationState", RpcTarget.All, characterAnim.CHARACTER_IDLE);
                legAnim.transform.GetComponent<PhotonView>().RPC("ChangeAnimationState", RpcTarget.All, legAnim.CHARACTER_WALKING);
            }
        }
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
}
