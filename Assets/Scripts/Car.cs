using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour
{
    public Route Route;
    public Transform BottomTransform;
    public Transform BodyTransform;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ParticleSystem _smokeVfx;
    [SerializeField] private float _danceValue;
    [SerializeField] private float _durationMultiplier;

    private void Start()
    {
        BodyTransform.DOLocalMoveY(_danceValue, 0.1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.TryGetComponent<Car>(out Car otherCar))
        {
            StopDance();
            _rigidbody.DOKill(false);

            Vector3 hitPoint = other.contacts[0].point;
            AddExplosionForce(hitPoint);
            _smokeVfx.Play();

            Game.Instance.RaiseCarCollide();
        }
    }

    public void Move(Vector3[] path)
    {
        _rigidbody.DOLocalPath(path, 2f * _durationMultiplier * path.Length)
            .SetLookAt(0.01f, false)
            .SetEase(Ease.Linear);
    }

    public void SetColor(Color color)
    {
        _meshRenderer.sharedMaterials[0].color = color;
    }

    public void StopDance()
    {
        BodyTransform.DOKill();
    }

    private void AddExplosionForce(Vector3 hitPoint)
    {
        _rigidbody.AddExplosionForce(400f, hitPoint, 3f);
        _rigidbody.AddForceAtPosition(Vector3.up * 2f, hitPoint, ForceMode.Impulse);
        _rigidbody.AddTorque(new Vector3(GetRandomAngle(), GetRandomAngle(), GetRandomAngle()));
    }

    private float GetRandomAngle()
    {
        float angle = 10f;
        float rand = Random.value;
        return (rand < 0.5f) ? -angle : angle;
    }
}
