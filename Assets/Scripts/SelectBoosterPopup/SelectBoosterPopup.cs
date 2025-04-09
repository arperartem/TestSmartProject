using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Data;
using SelectBoosterPopup.BoosterPoolManager;
using UnityEngine;
using Zenject;

namespace SelectBoosterPopup
{
    public class SelectBoosterPopup : IInitializable, IDisposable
    {
        private static readonly BoosterType[] DefaultBoosterTypes =
        {
            BoosterType.Shield,
            BoosterType.Bomb,
            BoosterType.RedPotion
        };

        private readonly SelectBoosterPopupView _view;
        private readonly BoosterDataHolder _boosterDataHolder;
        private readonly SelectBoosterFlow _selectBoosterFlow;
        private readonly IBoosterPoolManager _boosterPoolManager;
        
        private readonly WaitForSeconds _waitForSecondsOneTenth = new(0.1f);
        private WaitWhile _waitAnimatingBoosters;
        
        private  List<SelectBoosterView> BoosterPool => _boosterPoolManager.BoosterPool;
        private SelectBoosterView SelectedBoosterView => _boosterPoolManager.SelectedBooster;
        
        private BoosterType[] _lastSetBoosterTypes;

        public SelectBoosterPopup(SelectBoosterPopupView view, BoosterDataHolder boosterDataHolder,
            SelectBoosterFlow selectBoosterFlow, IBoosterPoolManager boosterPoolManager)
        {
            _view = view;
            _boosterDataHolder = boosterDataHolder;
            _selectBoosterFlow = selectBoosterFlow;
            _boosterPoolManager = boosterPoolManager;
        }

        public void Initialize()
        {
            _lastSetBoosterTypes = DefaultBoosterTypes;

            _boosterPoolManager.Initialize(DefaultBoosterTypes,
                _view.ContainerBoosters.transform);
            
            _view.ContainerBoosters.ReBuild();

            _view.ConfirmButton.gameObject.SetActive(false);
            _view.ClickedButton += OnClickedButton;
            _boosterPoolManager.SelectBoosterChanged += OnSelectBooster;

            _waitAnimatingBoosters = new WaitWhile(() => BoosterPool.Any(t => t.IsAnimating));
        }

        private void OnClickedButton(ButtonType type)
        {
            switch (type)
            {
                case ButtonType.Refresh:
                    _view.PlayRefreshButtonAnimation();

                    var newSet = _boosterDataHolder.BoosterTypes.GetRandomBoosters(3, 2, _lastSetBoosterTypes);
                    _lastSetBoosterTypes = newSet;

                    SelectedBoosterView?.SetSelect(false, true);

                    _view.ConfirmButton.gameObject.SetActive(false);
                    _view.StartCoroutine(RefreshCoroutine(newSet));
                    break;
                case ButtonType.Confirm:
                    _view.RefreshButton.gameObject.SetActive(false);
                    _view.ConfirmButton.PlayTapAnimation(() => _view.ConfirmButton.gameObject.SetActive(false));
                    OnClickConfirm();
                    break;
            }
        }

        private IEnumerator RefreshCoroutine(BoosterType[] boosterTypes)
        {
            _view.RefreshButton.interactable = false;

            yield return SetVisibleBoosters(false);

            _boosterPoolManager.UpdateBoosters(boosterTypes);

            yield return SetVisibleBoosters(true);

            _view.ContainerBoosters.ReBuild();
            _view.RefreshButton.interactable = true;
        }

        private IEnumerator SetVisibleBoosters(bool visible)
        {
            for (var i = 0; i < BoosterPool.Count; i++)
            {
                BoosterPool[i].SetVisibleAnimation(visible);
                yield return _waitForSecondsOneTenth;
            }

            yield return _waitAnimatingBoosters;
        }

        private void OnSelectBooster()
        {
            if (_view.ConfirmButton.gameObject.activeSelf == false)
            {
                _view.ConfirmButton.gameObject.SetActive(true);
                _view.ConfirmButton.PlayShowAnimation();
            }
        }
        
        private void OnClickConfirm()
        {
            for (var i = 0; i < BoosterPool.Count; i++)
            {
                if (BoosterPool[i] != SelectedBoosterView)
                    BoosterPool[i].SetVisibleAnimation(false);
            }

            SelectedBoosterView.ResetAnchors();
            _view.StartCoroutine(_selectBoosterFlow.SelectedBoosterFlow(SelectedBoosterView));
        }

        public void Dispose()
        {
            _view.ClickedButton -= OnClickedButton;
            _boosterPoolManager.SelectBoosterChanged -= OnSelectBooster;
        }
    }

    public enum ButtonType
    {
        Refresh,
        Confirm
    }
}