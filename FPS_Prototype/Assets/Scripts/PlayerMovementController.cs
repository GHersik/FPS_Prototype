using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] float speed = 1f;

    [Header("References")]
    [SerializeField] InputManager inputManager;

    private CharacterController playerCharacterController;
    private Vector3 movementDirection = Vector3.zero;

    private void Start()
    {
        playerCharacterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        Vector3 move = this.transform.right * movementDirection.x + transform.forward * movementDirection.z;
        playerCharacterController.Move(move * speed * Time.deltaTime);
    }

    private void UpdateMovementDirection(Vector2 arguments) => movementDirection = new Vector3(arguments.x, 0, arguments.y);

    private void OnEnable()
    {
        inputManager.MoveEvent += UpdateMovementDirection;
    }

    private void OnDisable()
    {
        inputManager.MoveEvent -= UpdateMovementDirection;
    }
}
