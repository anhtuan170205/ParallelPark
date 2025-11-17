using UnityEngine;

public class Car : MonoBehaviour
{
    public Route Route;
    public Transform BottomTransform;
    [SerializeField] private MeshRenderer _meshRenderer;

    public void SetColor(Color color)
    {
        _meshRenderer.sharedMaterials[0].color = color;
    }

}
