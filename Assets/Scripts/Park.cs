using UnityEngine;

public class Park : MonoBehaviour
{
    public Route Route;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ParticleSystem _enterParkVfx;
    private ParticleSystem.MainModule _vfxMainModule;

    private void Start()
    {
        _vfxMainModule = _enterParkVfx.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Car>(out Car car))
        {
            if (car.Route == Route)
            {
                Game.Instance.RaiseCarEnterPark(Route);
                StartVfx();
            }
        }
    }

    private void StartVfx()
    {
        _vfxMainModule.startColor = Route.CarColor;
        _enterParkVfx.Play();
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }
}
