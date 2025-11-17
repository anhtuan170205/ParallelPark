using UnityEngine;

public class Park : MonoBehaviour
{
    public Route Route;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }
}
