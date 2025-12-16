using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 5f; //toc chay
    [SerializeField] private float rotationSpeed = 10f; //toc do xoay

    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;

    private GameInput _inputActions;
    private Vector2 _inputVector; //luu gia tri input wasd
    public bool IsMovementPressed { get; private set; }
    public Vector2 CurrentInputVector => _inputVector;

    public CharacterController Controller { get; private set; }


    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        _inputActions = new GameInput();

        _states = new PlayerStateFactory(this);
        _currentState = _states.Idle();
        _currentState.EnterState();

        _inputActions.Gameplay.Move.performed += OnMoveInput;
        _inputActions.Gameplay.Move.canceled += OnMoveInput;
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        _inputVector = context.ReadValue<Vector2>();
        IsMovementPressed = _inputVector.x !=0 || _inputVector.y !=0;
    }
    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateState();
    }

    public void SwitchState(PlayerBaseState newState)
    {
        _currentState.ExitState();
        _currentState = newState;  
        _currentState.EnterState();
    }

    public void HandleMovement()
    {
        Vector3 move = new Vector3(_inputVector.x, 0, _inputVector.y); //vector di chuyen
        Controller.Move(move*moveSpeed*Time.deltaTime); //di chuyen nhan vat

        if(move != Vector3.zero) //tinh toan goc xoay va xoay
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        Controller.Move(Vector3.down*9.81f*Time.deltaTime); //trong luc
    }
}
