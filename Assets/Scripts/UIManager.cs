using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private LinesDrawer _linesDrawer;

    [Space]
    [SerializeField] private CanvasGroup _availableLineCanvasGroup;
    [SerializeField] private GameObject _availableLineHolder;
    [SerializeField] private Image _availableLineFill;
    private bool _isAvailableLineUIActive = false;

    [Space]
    [SerializeField] private Image _fadePanel;
    [SerializeField] private float _fadeDuration;

    private Route _activeRoute;

    private void Start()
    {
        _fadePanel.DOFade(0f, _fadeDuration).From(1f);
        
        _availableLineCanvasGroup.alpha = 0f;
        _linesDrawer.OnBeginDraw += HandleBeginDraw;
        _linesDrawer.OnDraw += HandleDraw;
        _linesDrawer.OnEndDraw += HandleEndDraw;
    }

    private void HandleBeginDraw(Route route)
    {
        _activeRoute = route;
        _availableLineFill.color = _activeRoute.CarColor;
        _availableLineFill.fillAmount = 1f;
        _availableLineCanvasGroup.DOFade(1f, 0.3f).From(0f);
        _isAvailableLineUIActive = true;
    }

    private void HandleDraw()
    {
        if (!_isAvailableLineUIActive) { return; }
        
        float maxLineLength = _activeRoute.MaxLineLength;
        float lineLength = _activeRoute.Line.Length;

        _availableLineFill.fillAmount = 1 - (lineLength / maxLineLength);
    }

    private void HandleEndDraw()
    {
        if (!_isAvailableLineUIActive) { return; }

        _isAvailableLineUIActive = false;
        _activeRoute = null;
        _availableLineCanvasGroup.DOFade(0f, 0.3f).From(1f);
    }
}
