using System;
using System.Collections.Generic;
using Core;
using DG.Tweening;
using Zenject;

namespace SideBar
{
    public class SideBar : IInitializable, IDisposable, ICellHolder, ISideBar
    {
        private const int AmountCells = 4;
        private const int AmountOpenCells = 1;
        
        private readonly SideBarView _view;
        private readonly FactoryUiView _factoryUiView;

        private bool _isVisible = true;
        private Tween _moveTween;

        public bool IsVisible => _isVisible;
        public bool IsAnimating => _moveTween.IsActive() && _moveTween.IsPlaying();

        public List<ICellView> Cells { get; } = new(4);
       

        public SideBar(SideBarView sideBarView, FactoryUiView factoryUiView)
        {
            _view = sideBarView;
            _factoryUiView = factoryUiView;
        }

        public void Initialize()
        {
            CreateCells();
            _view.VerticalLayout.ReBuild();
            _view.ArrowClicked += OnArrowClicked;
        }

        private void CreateCells()
        {
            for (int i = 0; i < AmountCells; i++)
            {
                var cellView = _factoryUiView.Create<CellView>(_view.VerticalLayout.transform);
                
                if(i < AmountOpenCells)
                    cellView.SetOpen();
                
                Cells.Add(cellView);
            }
        }
        
        public ICellView GetFirstAvailableCell()
        {
            return Cells[0];
        }

        private void OnArrowClicked()
        {
            SetVisible(!_isVisible);
        }

        public void SetVisible(bool show)
        {
            _moveTween?.Kill();

            if (show)
                _view.Panel.alpha = 1f;
            
            _moveTween = _view.MoveContainer
                .DOAnchorPosX(show ? _view.CacheBarPosX : 0f, 0.5f)
                .SetEase(show ? Ease.OutBack : Ease.InBack)
                .OnComplete(() =>
                {
                    if (!show)
                        _view.Panel.alpha = 0f;
                });

            _view.ArrowButton.image.rectTransform.SetFlipX(show);
            
            _isVisible = show;
        }

        public void Dispose()
        {
            _view.ArrowClicked -= OnArrowClicked;
        }
    }
}
