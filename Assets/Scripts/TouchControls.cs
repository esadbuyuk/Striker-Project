// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/TouchControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @TouchControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @TouchControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TouchControls"",
    ""maps"": [
        {
            ""name"": ""Touch1"",
            ""id"": ""17a688db-5aaf-4589-993d-9be9ec036009"",
            ""actions"": [
                {
                    ""name"": ""TouchInput"",
                    ""type"": ""PassThrough"",
                    ""id"": ""32ced9bc-ab86-4dd8-aabb-57cd00dc3a45"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchPress"",
                    ""type"": ""Button"",
                    ""id"": ""8063089c-d310-4a3c-b6d1-d26a15f24562"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""74dc88c5-8a4c-4923-ba58-4aeedba40709"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9f03649d-84a1-4538-839d-f4335068ed10"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd72f614-578c-49b8-bdcc-abb0e4b12ad8"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26a38251-bb8d-4b4c-8ac3-c9980a1624f8"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Touch1
        m_Touch1 = asset.FindActionMap("Touch1", throwIfNotFound: true);
        m_Touch1_TouchInput = m_Touch1.FindAction("TouchInput", throwIfNotFound: true);
        m_Touch1_TouchPress = m_Touch1.FindAction("TouchPress", throwIfNotFound: true);
        m_Touch1_TouchPosition = m_Touch1.FindAction("TouchPosition", throwIfNotFound: true);
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

    // Touch1
    private readonly InputActionMap m_Touch1;
    private ITouch1Actions m_Touch1ActionsCallbackInterface;
    private readonly InputAction m_Touch1_TouchInput;
    private readonly InputAction m_Touch1_TouchPress;
    private readonly InputAction m_Touch1_TouchPosition;
    public struct Touch1Actions
    {
        private @TouchControls m_Wrapper;
        public Touch1Actions(@TouchControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchInput => m_Wrapper.m_Touch1_TouchInput;
        public InputAction @TouchPress => m_Wrapper.m_Touch1_TouchPress;
        public InputAction @TouchPosition => m_Wrapper.m_Touch1_TouchPosition;
        public InputActionMap Get() { return m_Wrapper.m_Touch1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Touch1Actions set) { return set.Get(); }
        public void SetCallbacks(ITouch1Actions instance)
        {
            if (m_Wrapper.m_Touch1ActionsCallbackInterface != null)
            {
                @TouchInput.started -= m_Wrapper.m_Touch1ActionsCallbackInterface.OnTouchInput;
                @TouchInput.performed -= m_Wrapper.m_Touch1ActionsCallbackInterface.OnTouchInput;
                @TouchInput.canceled -= m_Wrapper.m_Touch1ActionsCallbackInterface.OnTouchInput;
                @TouchPress.started -= m_Wrapper.m_Touch1ActionsCallbackInterface.OnTouchPress;
                @TouchPress.performed -= m_Wrapper.m_Touch1ActionsCallbackInterface.OnTouchPress;
                @TouchPress.canceled -= m_Wrapper.m_Touch1ActionsCallbackInterface.OnTouchPress;
                @TouchPosition.started -= m_Wrapper.m_Touch1ActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.performed -= m_Wrapper.m_Touch1ActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.canceled -= m_Wrapper.m_Touch1ActionsCallbackInterface.OnTouchPosition;
            }
            m_Wrapper.m_Touch1ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TouchInput.started += instance.OnTouchInput;
                @TouchInput.performed += instance.OnTouchInput;
                @TouchInput.canceled += instance.OnTouchInput;
                @TouchPress.started += instance.OnTouchPress;
                @TouchPress.performed += instance.OnTouchPress;
                @TouchPress.canceled += instance.OnTouchPress;
                @TouchPosition.started += instance.OnTouchPosition;
                @TouchPosition.performed += instance.OnTouchPosition;
                @TouchPosition.canceled += instance.OnTouchPosition;
            }
        }
    }
    public Touch1Actions @Touch1 => new Touch1Actions(this);
    public interface ITouch1Actions
    {
        void OnTouchInput(InputAction.CallbackContext context);
        void OnTouchPress(InputAction.CallbackContext context);
        void OnTouchPosition(InputAction.CallbackContext context);
    }
}
