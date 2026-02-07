using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerInput.PlayerActions moveAction;

    CarController controller;

    private void Awake()
    {
        playerInput = new PlayerInput();
        moveAction = playerInput.Player;
    }
    void Start()
    {
        controller = GetComponent<CarController>();
    }
    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    void FixedUpdate()
    {
        controller.GetPlayerInput(moveAction.Move.ReadValue<Vector2>());
    }
}
