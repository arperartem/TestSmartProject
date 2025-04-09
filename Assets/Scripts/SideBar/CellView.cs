using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SideBar
{
    public class CellView : MonoBehaviour, ICellView
    {
        [SerializeField] private RectTransform backCell;
        [SerializeField] private Image icon;
        [SerializeField] private Image lockIcon;
        
        public Image Icon => icon;

        private Vector3 _cachePosition;
        private Vector2 _cacheSizeDelta;
        
        private void Awake()
        {
            _cachePosition = transform.position;
            _cacheSizeDelta = icon.rectTransform.sizeDelta;
        }

        public void PlayFly()
        {
            icon.transform.DOJump(_cachePosition, 5f, 1, 1f).SetEase(Ease.InCubic);
            icon.rectTransform.DOSizeDelta(_cacheSizeDelta, 1f).SetDelay(0.1f).SetEase(Ease.OutBack);
        }

        public void SetOpen()
        {
            lockIcon.SetAlpha(0f);
        }
    }
}