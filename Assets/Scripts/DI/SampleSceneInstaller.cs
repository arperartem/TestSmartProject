using CommonUI;
using Core;
using Core.Data;
using Particles;
using SelectBoosterPopup;
using SelectBoosterPopup.BoosterFly;
using SelectBoosterPopup.BoosterPoolManager;
using SideBar;
using UnityEngine;
using UpperPanel;
using Zenject;

namespace DI
{
    public class SampleSceneInstaller : MonoInstaller<SampleSceneInstaller>
    {
        [SerializeField] private Camera mainCamera;
        
        [SerializeField] private BoosterData[] boostersData;
        [SerializeField] private ViewUi[] prefabs;
        
        [SerializeField] private SelectBoosterPopupView selectBoosterPopupView;
        [SerializeField] private SideBarView sideBarView;
        [SerializeField] private ParticleHolderView particleHolderView;
        [SerializeField] private UpperPanelView upperPanelView;
        
        public override void InstallBindings()
        {
            Container.BindInstance(mainCamera);
            
            Container.BindInstance(new PoolPrefab(prefabs));
            
            Container.BindInstance(boostersData);
            Container.Bind<IParticlePlayer>().FromInstance(particleHolderView);
            Container.Bind<IUpperPanel>().FromInstance(upperPanelView);

            Container.BindInterfacesAndSelfTo<BoosterDataHolder>().AsSingle();
            Container.BindInterfacesTo<SelectBoosterPopup.SelectBoosterPopup>().AsSingle()
                .WithArguments(selectBoosterPopupView).NonLazy();
            Container.BindInterfacesTo<SideBar.SideBar>().AsSingle().WithArguments(sideBarView).NonLazy();

            Container.Bind<FactoryUiView>().AsSingle();
            Container.Bind<SelectBoosterFlow>().AsSingle();
            
            Container.BindInterfacesTo<BoosterPoolManager>().AsSingle();
            Container.Bind<BoosterFlyManager>().AsSingle();
        }
    }
}