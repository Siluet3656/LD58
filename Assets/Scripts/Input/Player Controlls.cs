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
                },
                {
                    ""name"": ""Skill 1"",
                    ""type"": ""Button"",
                    ""id"": ""3e6144ff-eee3-430d-b5d3-e530536d550e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill 2"",
                    ""type"": ""Button"",
                    ""id"": ""3a695507-5f82-4244-94d6-c10a903af88c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill 3"",
                    ""type"": ""Button"",
                    ""id"": ""c7cf7bf9-c01b-47ec-951d-28a922fa1413"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill 4"",
                    ""type"": ""Button"",
                    ""id"": ""6355f480-bdb5-470b-8290-3f7d65561b01"",
                    ""expectedControlType"": ""Button"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""3372f8b7-5749-4186-8cc8-18c6d7f9c18c"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48193f6f-aed2-439a-a125-abbb9ee52871"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8e22f14-778e-45ea-8471-6233f1c4650a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill 3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f24a899-ae5a-4fa7-900f-d9ce06718e00"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill 4"",
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
            m_UI_Skill1 = m_UI.FindAction("Skill 1", throwIfNotFound: true);
            m_UI_Skill2 = m_UI.FindAction("Skill 2", throwIfNotFound: true);
            m_UI_Skill3 = m_UI.FindAction("Skill 3", throwIfNotFound: true);
            m_UI_Skill4 = m_UI.FindAction("Skill 4", throwIfNotFound: true);
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
        private readonly InputAction m_UI_Skill1;
        private readonly InputAction m_UI_Skill2;
        private readonly InputAction m_UI_Skill3;
        private readonly InputAction m_UI_Skill4;
        public struct UIActions
        {
            private @PlayerControlls m_Wrapper;
            public UIActions(@PlayerControlls wrapper) { m_Wrapper = wrapper; }
            public InputAction @LBM => m_Wrapper.m_UI_LBM;
            public InputAction @RBM => m_Wrapper.m_UI_RBM;
            public InputAction @MousePosition => m_Wrapper.m_UI_MousePosition;
            public InputAction @Skill1 => m_Wrapper.m_UI_Skill1;
            public InputAction @Skill2 => m_Wrapper.m_UI_Skill2;
            public InputAction @Skill3 => m_Wrapper.m_UI_Skill3;
            public InputAction @Skill4 => m_Wrapper.m_UI_Skill4;
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
                    @Skill1.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSkill1;
                    @Skill1.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSkill1;
                    @Skill1.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSkill1;
                    @Skill2.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSkill2;
                    @Skill2.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSkill2;
                    @Skill2.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSkill2;
                    @Skill3.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSkill3;
                    @Skill3.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSkill3;
                    @Skill3.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSkill3;
                    @Skill4.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSkill4;
                    @Skill4.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSkill4;
                    @Skill4.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSkill4;
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
                    @Skill1.started += instance.OnSkill1;
                    @Skill1.performed += instance.OnSkill1;
                    @Skill1.canceled += instance.OnSkill1;
                    @Skill2.started += instance.OnSkill2;
                    @Skill2.performed += instance.OnSkill2;
                    @Skill2.canceled += instance.OnSkill2;
                    @Skill3.started += instance.OnSkill3;
                    @Skill3.performed += instance.OnSkill3;
                    @Skill3.canceled += instance.OnSkill3;
                    @Skill4.started += instance.OnSkill4;
                    @Skill4.performed += instance.OnSkill4;
                    @Skill4.canceled += instance.OnSkill4;
                }
            }
        }
        public UIActions @UI => new UIActions(this);
        public interface IUIActions
        {
            void OnLBM(InputAction.CallbackContext context);
            void OnRBM(InputAction.CallbackContext context);
            void OnMousePosition(InputAction.CallbackContext context);
            void OnSkill1(InputAction.CallbackContext context);
            void OnSkill2(InputAction.CallbackContext context);
            void OnSkill3(InputAction.CallbackContext context);
            void OnSkill4(InputAction.CallbackContext context);
        }
    }
}
