using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    //Call input manager actions
    private PlayerInput playerInput;
    private CharacterController Controller;
    //store actions for less bugs
    private InputAction attackAction;
    private InputAction moveAction;
    private Vector3 movement;
    private Rigidbody rb;
    private Vector3 playerVelocity;
    public float playerSpeed = 10.0F;

    //start this when an input controller is turned on
    private void Awake()
    {
        Controller = gameObject.GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions["Attack"];
        attackAction.ReadValue<float>();
        moveAction = playerInput.actions["Move"];

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }

   
}
