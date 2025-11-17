using UnityEngine;
using System;

public class UserInput : MonoBehaviour
{
    public event Action OnMouseDown;
    public event Action OnMouseUp;
    public event Action<Vector3> OnMouseMove;

    private bool _isMouseDown = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isMouseDown = true;
            OnMouseDown?.Invoke();
        }
        if (Input.GetMouseButtonUp(0))
        {
            _isMouseDown = false;
            OnMouseUp?.Invoke();
        }
        
        if (_isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            OnMouseMove?.Invoke(mousePosition);
        }
    }
}
