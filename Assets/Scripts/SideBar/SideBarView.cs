using System;
using CommonUI;
using UnityEngine;
using UnityEngine.UI;

namespace SideBar
{
    public class SideBarView : MonoBehaviour
    {
        public event Action ArrowClicked;
    
        [SerializeField] private Button arrowButton;
        [SerializeField] private RectTransform moveContainer;
        [SerializeField] private CanvasGroup panel;
        [SerializeField] private OnceVerticalLayout verticalLayout;
        
        public float CacheBarPosX { get; private set; }
        public Button ArrowButton => arrowButton;
        public RectTransform MoveContainer => moveContainer;
        public CanvasGroup Panel => panel;
        public OnceVerticalLayout VerticalLayout => verticalLayout;

        private void Awake()
        {
            CacheBarPosX = moveContainer.anchoredPosition.x;
            arrowButton.onClick.AddListener(() => ArrowClicked?.Invoke());
        }
    }
}
