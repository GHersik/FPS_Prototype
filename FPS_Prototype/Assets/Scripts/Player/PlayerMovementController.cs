using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] float speed = 1f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float groundCheckRadius = 0.4f;
    [SerializeField] LayerMask groundMask;

    [Header("References")]
    [SerializeField] InputManager inputManager;
    [SerializeField] Transform groundCheck;

    private CharacterController playerCharacterController;
    private Vector3 movementDirection = Vector3.zero;

    private Vector3 velocity = Vector3.zero;
    private bool isGrounded;

    private void Start()
    {
        playerCharacterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMove();
        PlayerFall();
    }

    private void PlayerMove()
    {
        Vector3 move = this.transform.right * movementDirection.x + transform.forward * movementDirection.z;
        playerCharacterController.Move(move * speed * Time.deltaTime);
    }

    private void PlayerFall()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        if (isGrounded)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        playerCharacterController.Move(velocity * Time.deltaTime);
    }

    private void UpdateMovementDirection(Vector2 arguments) => movementDirection = new Vector3(arguments.x, 0, arguments.y);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void OnEnable()
    {
        inputManager.MoveEvent += UpdateMovementDirection;
    }

    private void OnDisable()
    {
        inputManager.MoveEvent -= UpdateMovementDirection;
    }
}
