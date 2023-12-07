using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using UnityEngine;

public class ArduinoControls : MonoBehaviour
{

    [SerializeField]
    private float playerSpeed = 20.0f;
    //Attack variables
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;

    public int attackDamage;
    //controls variables
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Vector2 movementInput = Vector2.zero;
    private bool attacked = false;
    public float cooldown = 1.0f;

    public GameObject childWithAnimator;

    Animator animator;


    private SerialPort sp;
    [SerializeField] private string[] splitLine;
    private float x;
    private float y;
    private float button;

    private float movingX;
    private float movingY;
    private float buttonAttack;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = childWithAnimator.GetComponent<Animator>();

        sp = null;
        sp = new SerialPort("COM4", 57600);
        sp.Open();
        sp.ReadTimeout = 10;
        sp.DtrEnable = true;
        sp.RtsEnable = true;
    }
    void reset_Cooldown()
    {
        cooldown += 1.0f;
    }

    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if (playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movingX, 0, movingY);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            movingX = 0;
            movingY = 0;
        }

        if (move != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }


        // this is when a player is attacking
        if (buttonAttack == 0 && cooldown <= 0)
        {

            Debug.Log("attacking");


            //Detect enemies in range
            UnityEngine.Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayers);
            //Deal damage
            foreach (UnityEngine.Collider player in hitEnemies)
            {
                player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                Debug.Log("We hit" + player.name);
            }
            reset_Cooldown();
        }


        if (buttonAttack == 0)
        {
            //Play attack animation
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }


        //void OnDrawGizmosSelected()
        //{
        //    if (attackPoint == null)
        //        return;
        //    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        //}

        //playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (sp.IsOpen)
        {
            try
            {
                ReadCom();
                Move();
            }
            catch (System.Exception)
            {

            }
        }
        else
        {
            Move();
        }
    }

    void Move()
    {
        if (y >= 550)//left
        {
            Debug.Log("left");
            movingX = -1;
        }

        if (y <= 450)//right
        {
            Debug.Log("right");
            movingX = 1;
        }

        if (x <= 450)//up
        {
            Debug.Log("up");
            movingY = 1;
        }

        if (x >= 550)//down
        {
            Debug.Log("down");
            movingY = -1;
        }

        if (button == 0)//attack
        {
            Debug.Log("attack");
            buttonAttack = 0;
        }
        else
        {
            buttonAttack = 1;
        }
    }

    void ReadCom()
    {
        splitLine = sp.ReadLine().Split();
        if (!float.TryParse(splitLine[1], out x)) print("Failed to parse x");
        if (!float.TryParse(splitLine[2], out y)) print("Failed to parse y");
        if (!float.TryParse(splitLine[3], out button)) print("Failed to parse button");
    }
}