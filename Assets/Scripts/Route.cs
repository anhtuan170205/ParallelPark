using UnityEngine;
using System.Collections.Generic;

public class Route : MonoBehaviour
{
    [HideInInspector] public bool IsActive = true;
    [HideInInspector] public Vector3[] LinePoints;
    public float MaxLineLength;
    [SerializeField] private LinesDrawer _linesDrawer;

    public Line Line;
    public Park Park;
    public Car Car;

    [Space]
    [Header("Colors: ")]
    public Color CarColor;
    [SerializeField] private Color _lineColor;

    private void Start()
    {
        _linesDrawer.OnParkLinkedToLine += HandleParkLinkedToLine;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private void HandleParkLinkedToLine(Route route, List<Vector3> points)
    {
        if (route != this) { return; }
        LinePoints = points.ToArray();
        Game.Instance.RegisterRoute(this);
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
            Park.SetColor(CarColor);
        }
    }
#endif
}
