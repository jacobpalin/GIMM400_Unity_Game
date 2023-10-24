using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 20.0f;
    //[SerializeField]
    //private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    //Attack variables
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;
    
    public int attackDamage = 1;
    //controls variables
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Vector2 movementInput = Vector2.zero;
    private bool attacked = false;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context) 
    {
        movementInput = context.ReadValue<Vector2>();    
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        attacked = context.action.triggered;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // this is when a player is attacking
        if (attacked == true)
        {
            //Play attack animation

            //Detect enemies in range
            UnityEngine.Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayers);
            //Deal damage
            foreach(UnityEngine.Collider player in hitEnemies)
            {
                player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                Debug.Log("We hit" + player.name);
            }
        }
        //void OnDrawGizmosSelected()
        //{
        //    if (attackPoint == null)
        //        return;
        //    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        //}

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
