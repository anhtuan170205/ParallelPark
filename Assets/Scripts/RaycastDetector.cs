using UnityEngine;
using UnityEngine.InputSystem;

public struct ContactInfo
{
    public bool Contacted;
    public Vector3 Point;
    public Collider Collider;
    public Transform Transform;
}

public class RaycastDetector
{
    public ContactInfo RayCast(int layerMask)
    {
        Mouse mouse = Mouse.current;
        if (mouse == null)
        {
            return new ContactInfo
            {
                Contacted = false,
                Point = Vector3.zero,
                Collider = null,
                Transform = null
            };
        }

        Vector2 mousePos = mouse.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, 0f));

        bool hit = Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, 1 << layerMask);

        return new ContactInfo
        {
            Contacted = hit,
            Point = hit ? hitInfo.point : Vector3.zero,
            Collider = hit ? hitInfo.collider : null,
            Transform = hit ? hitInfo.transform : null
        };
    }
}
