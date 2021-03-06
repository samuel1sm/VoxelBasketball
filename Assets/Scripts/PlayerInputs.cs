// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""Actions"",
            ""id"": ""ed254dcf-4285-444f-9379-c0077598054e"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9a4c962c-5ed4-4198-9cc3-f1904f8cc6fb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FirstAction"",
                    ""type"": ""Button"",
                    ""id"": ""681205b1-5ffc-43e1-b20f-1adf44d8714e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondAction"",
                    ""type"": ""Button"",
                    ""id"": ""dc307e37-fc34-4294-b157-c0dcf446bff8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reset"",
                    ""type"": ""Button"",
                    ""id"": ""86ea37df-01e5-4e65-b5e6-9575cdb16fce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""9a525dc2-8ee3-48da-a58f-772f53f8b4cf"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""34214856-28b4-4bec-9f88-118db6f8ce93"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""64648ae0-efec-4673-a95a-958145ad4bec"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3131b7b6-74e9-4eee-8bb0-d3880614fc09"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""001add9e-5017-48df-be1e-b91ea7eba2f7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1621c33f-b0d9-4a79-bf5a-b2bc4789ff54"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FirstAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a41703e-b2ec-4b6e-8afb-58c6d5f082bb"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b99b9fef-4d53-4a9c-a71b-a4a93daceb72"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reset"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Actions
        m_Actions = asset.FindActionMap("Actions", throwIfNotFound: true);
        m_Actions_Movement = m_Actions.FindAction("Movement", throwIfNotFound: true);
        m_Actions_FirstAction = m_Actions.FindAction("FirstAction", throwIfNotFound: true);
        m_Actions_SecondAction = m_Actions.FindAction("SecondAction", throwIfNotFound: true);
        m_Actions_Reset = m_Actions.FindAction("Reset", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Actions
    private readonly InputActionMap m_Actions;
    private IActionsActions m_ActionsActionsCallbackInterface;
    private readonly InputAction m_Actions_Movement;
    private readonly InputAction m_Actions_FirstAction;
    private readonly InputAction m_Actions_SecondAction;
    private readonly InputAction m_Actions_Reset;
    public struct ActionsActions
    {
        private @PlayerInputs m_Wrapper;
        public ActionsActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Actions_Movement;
        public InputAction @FirstAction => m_Wrapper.m_Actions_FirstAction;
        public InputAction @SecondAction => m_Wrapper.m_Actions_SecondAction;
        public InputAction @Reset => m_Wrapper.m_Actions_Reset;
        public InputActionMap Get() { return m_Wrapper.m_Actions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionsActions set) { return set.Get(); }
        public void SetCallbacks(IActionsActions instance)
        {
            if (m_Wrapper.m_ActionsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnMovement;
                @FirstAction.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnFirstAction;
                @FirstAction.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnFirstAction;
                @FirstAction.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnFirstAction;
                @SecondAction.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSecondAction;
                @SecondAction.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSecondAction;
                @SecondAction.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSecondAction;
                @Reset.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnReset;
                @Reset.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnReset;
                @Reset.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnReset;
            }
            m_Wrapper.m_ActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @FirstAction.started += instance.OnFirstAction;
                @FirstAction.performed += instance.OnFirstAction;
                @FirstAction.canceled += instance.OnFirstAction;
                @SecondAction.started += instance.OnSecondAction;
                @SecondAction.performed += instance.OnSecondAction;
                @SecondAction.canceled += instance.OnSecondAction;
                @Reset.started += instance.OnReset;
                @Reset.performed += instance.OnReset;
                @Reset.canceled += instance.OnReset;
            }
        }
    }
    public ActionsActions @Actions => new ActionsActions(this);
    public interface IActionsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnFirstAction(InputAction.CallbackContext context);
        void OnSecondAction(InputAction.CallbackContext context);
        void OnReset(InputAction.CallbackContext context);
    }
}
