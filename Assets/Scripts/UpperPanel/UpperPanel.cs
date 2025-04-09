using DG.Tweening;
using UnityEngine;

namespace UpperPanel
{
    public class UpperPanelView : MonoBehaviour, IUpperPanel
    {
        [SerializeField] private CanvasGroup canvasGroup;
        
        public void Hide()
        {
            canvasGroup.DOFade(0f, 1.5f);
        }
    }
}