using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 20.0f;
    //Attack variables
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;
    
    public int attackDamage = 26;
    //controls variables
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Vector2 movementInput = Vector2.zero;
    private bool attacked = false;

    public Animator animator;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
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
        if (playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        //animate movement
        if(move == Vector3.zero)
        {
            //idle
            animator.SetFloat("speed", 0);

        }
        else if(move != Vector3.zero)
        {
            animator.SetFloat("speed", 1);
        }

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

        //playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
