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
    public event Action OnCarCollide;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        OnCarCollide += HandleCarCollide;
    }

    public void RaiseCarEnterPark(Route route)
    {
        OnCarEnterPark?.Invoke(route);
    }

    public void RaiseCarCollide()
    {
        OnCarCollide?.Invoke();
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

    private void HandleCarCollide()
    {
        Debug.Log("Game Over!");
        DOVirtual.DelayedCall(2f, () =>
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentLevel); 
        });
    }
}
