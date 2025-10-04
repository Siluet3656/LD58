// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/Player Controlls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Input
{
    public class @PlayerControlls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControlls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player Controlls"",
    ""maps"": [
        {
            ""name"": ""UI"",
            ""id"": ""91be2380-602a-4b30-a81c-951e03430888"",
            ""actions"": [
                {
                    ""name"": ""LBM"",
                    ""type"": ""Button"",
                    ""id"": ""33dd5cce-32b8-4f49-bd7c-54785df2e477"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RBM"",
                    ""type"": ""Button"",
                    ""id"": ""f1108cdc-2dd1-4fb2-9cbe-2b5b83beaa42"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""b7f24009-314a-4bc0-807f-b3cc7523d752"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7b138625-de2f-4f84-b8f6-643f1ded5919"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LBM"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5014725-e9f2-4ccd-b075-658672012e49"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ab23125-5ba9-499c-9680-f79b1989f816"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RBM"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // UI
            m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
            m_UI_LBM = m_UI.FindAction("LBM", throwIfNotFound: true);
            m_UI_RBM = m_UI.FindAction("RBM", throwIfNotFound: true);
            m_UI_MousePosition = m_UI.FindAction("MousePosition", throwIfNotFound: true);
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

        // UI
        private readonly InputActionMap m_UI;
        private IUIActions m_UIActionsCallbackInterface;
        private readonly InputAction m_UI_LBM;
        private readonly InputAction m_UI_RBM;
        private readonly InputAction m_UI_MousePosition;
        public struct UIActions
        {
            private @PlayerControlls m_Wrapper;
            public UIActions(@PlayerControlls wrapper) { m_Wrapper = wrapper; }
            public InputAction @LBM => m_Wrapper.m_UI_LBM;
            public InputAction @RBM => m_Wrapper.m_UI_RBM;
            public InputAction @MousePosition => m_Wrapper.m_UI_MousePosition;
            public InputActionMap Get() { return m_Wrapper.m_UI; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
            public void SetCallbacks(IUIActions instance)
            {
                if (m_Wrapper.m_UIActionsCallbackInterface != null)
                {
                    @LBM.started -= m_Wrapper.m_UIActionsCallbackInterface.OnLBM;
                    @LBM.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnLBM;
                    @LBM.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnLBM;
                    @RBM.started -= m_Wrapper.m_UIActionsCallbackInterface.OnRBM;
                    @RBM.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnRBM;
                    @RBM.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnRBM;
                    @MousePosition.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMousePosition;
                    @MousePosition.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMousePosition;
                    @MousePosition.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMousePosition;
                }
                m_Wrapper.m_UIActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @LBM.started += instance.OnLBM;
                    @LBM.performed += instance.OnLBM;
                    @LBM.canceled += instance.OnLBM;
                    @RBM.started += instance.OnRBM;
                    @RBM.performed += instance.OnRBM;
                    @RBM.canceled += instance.OnRBM;
                    @MousePosition.started += instance.OnMousePosition;
                    @MousePosition.performed += instance.OnMousePosition;
                    @MousePosition.canceled += instance.OnMousePosition;
                }
            }
        }
        public UIActions @UI => new UIActions(this);
        public interface IUIActions
        {
            void OnLBM(InputAction.CallbackContext context);
            void OnRBM(InputAction.CallbackContext context);
            void OnMousePosition(InputAction.CallbackContext context);
        }
    }
}
