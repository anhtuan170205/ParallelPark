using UnityEngine;

public struct ContactInfo
{
    public bool Contacted;
    public Vector3 Point;
    public Collider Collider;
    public Transform Transform;
}
public class RaycastDetector : MonoBehaviour
{
    public ContactInfo RayCast(int layerMask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, layerMask);

        return new ContactInfo
        {
            Contacted = hit,
            Point = hit ? hitInfo.point : Vector3.zero,
            Collider = hit ? hitInfo.collider : null,
            Transform = hit ? hitInfo.transform : null
        };
    }
}
