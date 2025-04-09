using System;
using CommonUI;
using Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SelectBoosterPopup
{
    public class SelectBoosterView : ViewUi
    {
        private Action _callback;
        
        [SerializeField] private Button button;
        [SerializeField] private Image icon;
        [SerializeField] private Image outline;
        [SerializeField] private RectTransform root;
        [SerializeField] private Image checkmark;

        private Transform _transform;
        private Tween _animTween;

        private BoosterType _type;

        public bool IsAnimating => _animTween.IsActive() && _animTween.IsPlaying();

        public RectTransform Root => root;
        public Image Icon => icon;

        private void Awake()
        {
            button.onClick.AddListener(() => _callback?.Invoke());
            _transform = transform;
        }

        public void Initialize(Sprite sprite, Action callback)
        {
            icon.sprite = sprite;
            _callback = callback;
        }

        public void SetVisibleAnimation(bool visible, bool instant = false)
        {
            _animTween.Kill();

            if (visible)
                _animTween = _transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
            else
                _animTween = _transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
        }

        public void SetSelect(bool select, bool instant = false)
        {
            outline.DOKill();

            if (instant)
                outline.SetAlpha(select ? 1f : 0f);
            else
            {
                outline.SetAlpha(select ? 0f : 1f);
                outline.DOFade(select ? 1f : 0f, 0.15f);  
            }
        }

        public void SetPicked(bool value)
        {
            checkmark.SetAlpha(value ? 1f : 0f);
            if (value)
            {
                checkmark.transform.localScale = Vector3.zero;
                checkmark.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBounce);
            }
        }

        public void ResetAnchors()
        {
            var pos = _transform.localPosition;
            root.anchorMin = new Vector2(0.5f, 0.5f);
            root.anchorMax = new Vector2(0.5f, 0.5f);
            root.pivot = new Vector2(0.5f, 0.5f);
            _transform.localPosition = pos;
        }
    }
}