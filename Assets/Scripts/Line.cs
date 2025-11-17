using UnityEngine;
using System.Collections.Generic;

public class Line : MonoBehaviour
{
    public LineRenderer LineRenderer;
    [SerializeField] private float _minPointDistance;

    [HideInInspector] public List<Vector3> Points = new List<Vector3>();
    [HideInInspector] public int pointsCount = 0;

    private float _pointFixedYAxis;

    private void Start()
    {
        _pointFixedYAxis = LineRenderer.GetPosition(0).y;
        Clear();
    }

    private void Init()
    {
        gameObject.SetActive(true);
    }

    private void Clear()
    {
        gameObject.SetActive(false);
        LineRenderer.positionCount = 0;
        pointsCount = 0;
        Points.Clear();
    }

    public void AddPoint(Vector3 newPoint)
    {
        newPoint.y = _pointFixedYAxis;

        if (pointsCount >= 1 && Vector3.Distance(newPoint, GetLastPoint()) < _minPointDistance) { return; }

        Points.Add(newPoint);
        pointsCount++;

        LineRenderer.positionCount = pointsCount;
        LineRenderer.SetPosition(pointsCount - 1, newPoint);
    }

    private Vector3 GetLastPoint()
    {
        return LineRenderer.GetPosition(pointsCount - 1);
    }

    public void SetColor(Color color)
    {
        LineRenderer.sharedMaterials[0].color = color;
    }
}
