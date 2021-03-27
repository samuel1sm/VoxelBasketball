using UnityEngine;
using Utils;


public class InputManager : CharacterControls
{
    private PlayerInputs _playerInput;

    private bool IsAttacking = false;
    
    private void Awake()
    {
        _playerInput = new PlayerInputs();
    }

    private void Start()
    {
        _playerInput.Actions.FirstAction.started += _ => OnFirstActionPressed(ButtonInputTypes.Started);
        _playerInput.Actions.FirstAction.performed += _ => OnFirstActionPressed(ButtonInputTypes.Performed);
        _playerInput.Actions.FirstAction.canceled += _ => OnFirstActionPressed(ButtonInputTypes.Canceled);

        _playerInput.Actions.SecondAction.started += _ => OnSecondActionPressed(ButtonInputTypes.Started);
        _playerInput.Actions.SecondAction.performed += _ => OnSecondActionPressed(ButtonInputTypes.Performed);
        _playerInput.Actions.SecondAction.canceled += _ => OnSecondActionPressed(ButtonInputTypes.Canceled);

    }

    public override void MovementActivation(ButtonInputTypes types)
    {
        IsAttacking = types == ButtonInputTypes.Started;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public override Vector3 GetMovement()
    {
        if (IsAttacking)
            return Vector3.zero;
        
        return Vector22Vector3(_playerInput.Actions.Movement.ReadValue<Vector2>());
    }

    private Vector3 Vector22Vector3(Vector2 vector2)
    {
        return new Vector3(vector2.x, 0, vector2.y);
    }
}
