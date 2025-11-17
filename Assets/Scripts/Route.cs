using UnityEngine;

public class Route : MonoBehaviour
{
    [HideInInspector] public bool IsActive = true;

    public Line Line;
    public Park Park;
    public Car Car;

    [Space]
    [Header("Colors: ")]
    public Color CarColor;
    [SerializeField] private Color _lineColor;

    public void Deactivate()
    {
        IsActive = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() 
    {
        if (!Application.isPlaying && Line != null && Car != null && Park != null)
        {
            Line.LineRenderer.SetPosition(0, Car.BottomTransform.position);
            Line.LineRenderer.SetPosition(1, Park.transform.position);

            Line.SetColor(_lineColor);
            Car.SetColor(CarColor);
            Park.SetColor(_lineColor);
        }
    }

#endif
}
