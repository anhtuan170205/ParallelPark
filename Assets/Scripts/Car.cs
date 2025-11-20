using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour
{
    public Route Route;
    public Transform BottomTransform;
    public Transform BodyTransform;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _danceValue;
    [SerializeField] private float _durationMultiplier;

    private void Start()
    {
        BodyTransform.DOLocalMoveY(_danceValue, 0.1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
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
}
