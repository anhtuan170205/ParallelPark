using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer LineRenderer;

    public void SetColor(Color color)
    {
        LineRenderer.sharedMaterials[0].color = color;
    }
}
