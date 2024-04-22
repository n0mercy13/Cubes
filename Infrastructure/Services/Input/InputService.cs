using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Codebase.Infrastructure
{
    public partial class InputService
    {
        private readonly InputActions _inputs;

        public InputService(InputActions inputs)
        {
            _inputs = inputs;
            _inputs.Enable();

            _inputs.Gameplay.Select.performed += OnSelectPerformed;
        }

        private Vector2 MousePosition =>
            Mouse.current.position.ReadValue();

        private void OnSelectPerformed(InputAction.CallbackContext context)
        {
            if (context.phase.Equals(InputActionPhase.Performed))
                Selected.Invoke(MousePosition);
        }
    }

    public partial class InputService : IGameplayInput
    {
        public event Action<Vector2> Selected = delegate { };
    }

    public partial class InputService : IDisposable
    {
        public void Dispose()
        {
            _inputs.Gameplay.Select.performed -= OnSelectPerformed;
        }
    }
}
