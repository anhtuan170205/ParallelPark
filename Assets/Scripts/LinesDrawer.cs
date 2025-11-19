using UnityEngine;
using System.Collections.Generic;
using System;

public class LinesDrawer : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;
    [SerializeField] private int _interactableLayer;

    private Line _currentLine;
    private Route _currentRoute;
    private RaycastDetector _raycastDetector = new RaycastDetector();

    public Action<Route, List<Vector3>> OnParkLinkedToLine;

    private void Start()
    {
        _userInput.OnMouseDown += HandleMouseDown;
        _userInput.OnMouseUp += HandleMouseUp;
        _userInput.OnMouseMove += HandleMouseMove;
    }

    private void HandleMouseDown()
    {
        ContactInfo contactInfo = _raycastDetector.RayCast(_interactableLayer);

        if (contactInfo.Contacted)
        {
            bool isCar = contactInfo.Collider.TryGetComponent<Car>(out Car car);
            if (isCar && car.Route.IsActive)
            {
                _currentRoute = car.Route;
                _currentLine = _currentRoute.Line;
                _currentLine.Init();
            }
        }
    }

    private void HandleMouseMove(Vector3 mousePosition)
    {
        if (_currentRoute == null) { return; }
        ContactInfo contactInfo = _raycastDetector.RayCast(_interactableLayer);

        if (contactInfo.Contacted)
        {
            Vector3 newPoint = contactInfo.Point;
            _currentLine.AddPoint(newPoint);

            bool isPark = contactInfo.Collider.TryGetComponent<Park>(out Park park);
            if (isPark)
            {
                Route parkRoute = park.Route;
                if (parkRoute == _currentRoute)
                {
                    _currentLine.AddPoint(contactInfo.Transform.position);                    
                }
                else
                {
                    _currentLine.Clear();
                }
                HandleMouseUp();
            }
        }
    }

    private void HandleMouseUp()
    {
        if (_currentRoute == null) { return; }
        
        ContactInfo contactInfo = _raycastDetector.RayCast(_interactableLayer);

        if (contactInfo.Contacted)
        {
            bool isPark = contactInfo.Collider.TryGetComponent<Park>(out Park park);
            if (_currentLine.PointsCount < 2 || !isPark)
            {
                _currentLine.Clear();
            }
            else
            {
                OnParkLinkedToLine?.Invoke(_currentRoute, _currentLine.Points);
                _currentRoute.Deactivate();
            }
        }
        else
        {
            _currentLine.Clear();
        }
        ResetDrawer();
    }

    private void ResetDrawer()
    {
        _currentLine = null;
        _currentRoute = null;
    }
}
