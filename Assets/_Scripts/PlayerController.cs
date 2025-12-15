using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerControll : MonoBehaviour
{

    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 5f; //toc chay
    [SerializeField] private float rotationSpeed = 10f; //toc do xoay

    private  CharacterController _controller;
    private GameInput _inputActions;
    private Vector2 _inputVector; //luu gia tri input wasd

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _inputActions = new GameInput();

        _inputActions.Gameplay.Move.performed += ctx => _inputVector = ctx.ReadValue<Vector2>();
        _inputActions.Gameplay.Move.canceled += ctx => _inputVector = Vector2.zero;
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
        HandledMovement();   
    }

    private void HandledMovement()
    {
        Vector3 move = new Vector3(_inputVector.x, 0, _inputVector.y); //vector di chuyen
        _controller.Move(move*moveSpeed*Time.deltaTime); //di chuyen nhan vat

        if(move != Vector3.zero) //tinh toan goc xoay va xoay
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        _controller.Move(Vector3.down*9.81f*Time.deltaTime); //trong luc
    }
}
