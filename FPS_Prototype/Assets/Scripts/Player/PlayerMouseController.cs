using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseController : MonoBehaviour
{
    [Header("Control Options")]
    [SerializeField] float mouseSensitivity = 100f;


    [Header("References")]
    [SerializeField] InputManager inputManager;
    [SerializeField] Camera playerCamera;


    private Vector2 pointDirection = Vector2.zero;
    private float rotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        PointTowards();
    }

    private void PointTowards()
    {
        rotation -= pointDirection.y;
        rotation = Mathf.Clamp(rotation, -90, 90);

        playerCamera.transform.localRotation = Quaternion.Euler(rotation, 0, 0);
        this.transform.Rotate(Vector3.up * pointDirection.x);
    }

    private void UpdatePointDirection(Vector2 arguments) => pointDirection = new Vector2(arguments.x * mouseSensitivity * Time.deltaTime, arguments.y * mouseSensitivity * Time.deltaTime);

    private void OnEnable()
    {
        inputManager.PointerMoveEvent += UpdatePointDirection;
    }

    private void OnDisable()
    {
        inputManager.PointerMoveEvent -= UpdatePointDirection;
    }
}
