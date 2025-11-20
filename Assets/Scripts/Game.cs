using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using DG.Tweening;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [HideInInspector] public List<Route> ReadyRoutes = new List<Route>();
    private int _totalRoutesCount;
    private int _successfullParksCount;

    public event Action<Route> OnCarEnterPark;

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
        _successfullParksCount = 0;
        OnCarEnterPark += HandleCarEnterPark;
    }

    public void RaiseCarEnterPark(Route route)
    {
        OnCarEnterPark?.Invoke(route);
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

    private void HandleCarEnterPark(Route route)
    {
        route.Car.StopDance();
        _successfullParksCount++;

        if (_successfullParksCount == _totalRoutesCount)
        {
            Debug.Log("Level Completed!");
            int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
            DOVirtual.DelayedCall(1.3f, () =>
            {
                if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(nextLevelIndex);
                }
                else
                {
                    Debug.LogWarning("No more levels to load.");
                }
            });
        }
    }
}
