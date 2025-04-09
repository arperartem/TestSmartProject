using System;
using CommonUI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SideBar
{
    public class SideBarView : MonoBehaviour
    {
        internal event Action ArrowClicked;
    
        [SerializeField] private Button arrowButton;
        [SerializeField] private RectTransform moveContainer;
        [SerializeField] private CanvasGroup panel;
        [SerializeField] private OnceVerticalLayout verticalLayout;
        [SerializeField] private CellView cellPrefab;
        
        internal float CacheBarPosX { get; private set; }
        internal Button ArrowButton => arrowButton;
        internal RectTransform MoveContainer => moveContainer;
        internal CanvasGroup Panel => panel;
        internal OnceVerticalLayout VerticalLayout => verticalLayout;
        internal CellView CellPrefab => cellPrefab;

        private void Awake()
        {
            CacheBarPosX = moveContainer.anchoredPosition.x;
            arrowButton.onClick.AddListener(() => ArrowClicked?.Invoke());
        }
    
    
    }
}
