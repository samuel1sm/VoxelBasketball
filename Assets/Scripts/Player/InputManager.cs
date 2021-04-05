using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;


public class InputManager : CharacterControls
{
    private PlayerInputs _playerInput;

    
    private void Awake()
    {
        _playerInput = new PlayerInputs();
    }

    private void Start()
    {
        _playerInput.Actions.FirstAction.started += _ => OnFirstActionPressed(ButtonInputTypes.Started);
        _playerInput.Actions.FirstAction.performed += _ => OnFirstActionPressed(ButtonInputTypes.Performed);
        _playerInput.Actions.FirstAction.canceled += _ => OnFirstActionPressed(ButtonInputTypes.Canceled);
        _playerInput.Actions.Reset.started += _ => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        _playerInput.Actions.SecondAction.started += _ => OnSecondActionPressed(ButtonInputTypes.Started);
        _playerInput.Actions.SecondAction.performed += _ => OnSecondActionPressed(ButtonInputTypes.Performed);
        _playerInput.Actions.SecondAction.canceled += _ => OnSecondActionPressed(ButtonInputTypes.Canceled);

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

        return Vector22Vector3(_playerInput.Actions.Movement.ReadValue<Vector2>());
    }

    private Vector3 Vector22Vector3(Vector2 vector2)
    {
        return new Vector3(vector2.x, 0, vector2.y);
    }
}
