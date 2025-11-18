using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public event Action OnMouseDown;
    public event Action OnMouseUp;
    public event Action<Vector3> OnMouseMove;

    private bool _isMouseDown = false;

    private void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse == null) return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            _isMouseDown = true;
            OnMouseDown?.Invoke();
        }

        if (mouse.leftButton.wasReleasedThisFrame)
        {
            _isMouseDown = false;
            OnMouseUp?.Invoke();
        }

        if (_isMouseDown)
        {
            Vector2 mousePos = mouse.position.ReadValue();
            OnMouseMove?.Invoke(new Vector3(mousePos.x, mousePos.y, 0f));
        }
    }
}
