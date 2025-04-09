using System;
using Core;
using DG.Tweening;
using UnityEngine;

namespace SelectBoosterPopup.BoosterFly
{
    public class BoosterFlyManager
    {
        private readonly FactoryUiView _factoryUiView;

        public BoosterFlyManager(FactoryUiView factoryUiView)
        {
            _factoryUiView = factoryUiView;
        }
        
        public void PlayBoosterFly(RectTransform source, RectTransform target, Sprite sprite, Transform parent, Action callback)
        {
            var view = _factoryUiView.Create<BoosterFlyViewUi>(parent);
            
            view.RectTransform.anchoredPosition = source.anchoredPosition;
            view.RectTransform.sizeDelta = source.sizeDelta;
            view.RectTransform.localScale = source.localScale;
            view.Image.sprite = sprite;

            DOTween.Sequence().Join(view.RectTransform.DOJump(target.position, 5f, 1, 1f).SetEase(Ease.InCubic))
                .Join(view.RectTransform.DOSizeDelta(target.rect.size, 1f).SetEase(Ease.OutBack))
                .Join(view.RectTransform.DOScale(target.localScale, 1f)).OnComplete(() =>
                {
                    view.Image.SetAlpha(0);
                    callback?.Invoke();
                });
        }
    }
}