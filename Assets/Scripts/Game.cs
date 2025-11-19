using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [HideInInspector] public List<Route> ReadyRoutes = new List<Route>();
    private int _totalRoutesCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _totalRoutesCount = transform.GetComponentsInChildren<Route>().Length;
    }

    public void RegisterRoute(Route route)
    {
        ReadyRoutes.Add(route);

        if (ReadyRoutes.Count == _totalRoutesCount)
        {
            MoveAllCars();
        }
    }

    private void MoveAllCars()
    {
        foreach (var route in ReadyRoutes)
        {
            route.Car.Move(route.LinePoints);
        }
    }
}
