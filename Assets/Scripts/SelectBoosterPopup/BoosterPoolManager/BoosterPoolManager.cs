using System;
using System.Collections.Generic;
using Booster;
using Core;
using Data;
using UnityEngine;

namespace SelectBoosterPopup.BoosterPoolManager
{
    public class BoosterPoolManager : IBoosterPoolManager
    {
        public event Action SelectBoosterChanged;
        
        private readonly BoosterDataHolder _boosterDataHolder;
        private readonly FactoryUiView _factoryUiView;
        private readonly List<SelectBoosterView> _boosterPool = new();
        private readonly Dictionary<BoosterType, SelectBoosterView> _boosterViewMap = new();

        public List<SelectBoosterView> BoosterPool => _boosterPool;
        public SelectBoosterView SelectedBooster { get; private set; }

        private SelectBoosterView _prefab;
        private Transform _containerBoosters;

        public BoosterPoolManager(BoosterDataHolder boosterDataHolder,
            FactoryUiView factoryUiView)
        {
            _boosterDataHolder = boosterDataHolder;
            _factoryUiView = factoryUiView;
        }
        
        public void Initialize(BoosterType[] initialBoosters, Transform containerBoosters)
        {
            _containerBoosters = containerBoosters;
            _boosterPool.Clear();
            _boosterViewMap.Clear();

            for (var i = 0; i < initialBoosters.Length; i++)
                CreateOrUpdateBoosterView(initialBoosters[i]);
        }

        public void UpdateBoosters(BoosterType[] newBoosters)
        {
            _boosterViewMap.Clear();
        
            for (var i = 0; i < newBoosters.Length; i++)
                CreateOrUpdateBoosterView(newBoosters[i]);
        
            SelectedBooster = null;
        }

        private void CreateOrUpdateBoosterView(BoosterType boosterType)
        {
            if (_boosterDataHolder.BoosterDataMap.TryGetValue(boosterType, out var boosterData) == false)
                throw new KeyNotFoundException("Booster not found");

            SelectBoosterView boosterView;
        
            if (_boosterPool.Count > _boosterViewMap.Count)
            {
                boosterView = _boosterPool[_boosterViewMap.Count];
                boosterView.Initialize(boosterData.Sprite, () => OnBoosterClick(boosterType));
            }
            else
            {
                boosterView = _factoryUiView.Create<SelectBoosterView>(_containerBoosters);
                boosterView.Initialize(boosterData.Sprite, () => OnBoosterClick(boosterType));
                _boosterPool.Add(boosterView);
            }
            _boosterViewMap[boosterType] = boosterView;
        }

        private void OnBoosterClick(BoosterType boosterType)
        {
            if (_boosterViewMap.TryGetValue(boosterType, out var boosterView) == false)
                throw new KeyNotFoundException("Booster view not found");

            if (SelectedBooster != null && SelectedBooster != boosterView)
                SelectedBooster.SetSelect(false);
        
            SelectedBooster = boosterView;
            boosterView.SetSelect(true);
            SelectBoosterChanged?.Invoke();
        }
    }
}
