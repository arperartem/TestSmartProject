using System;
using CommonUI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SelectBoosterPopup
{
    public class SelectBoosterPopupView : MonoBehaviour
    {
        internal event Action<ButtonType> ClickedButton;
        
        [SerializeField] private SelectBoosterView[] boosters;
        [SerializeField] private ConfirmButtonView confirmButton;
        [SerializeField] private Button refreshButton;
        [SerializeField] private OnceHorizontalLayout containerBoosters;
        [SerializeField] private SelectBoosterView selectBoosterPrefab;
        
        public OnceHorizontalLayout ContainerBoosters => containerBoosters;
        public SelectBoosterView SelectBoosterPrefab => selectBoosterPrefab;

        public ConfirmButtonView ConfirmButton => confirmButton;
        public Button RefreshButton => refreshButton;

        private void Awake()
        {
            refreshButton.onClick.AddListener(() => ClickedButton?.Invoke(ButtonType.Refresh));
            confirmButton.Button.onClick.AddListener(() => ClickedButton?.Invoke(ButtonType.Confirm));
        }

        internal void PlayRefreshButtonAnimation()
        {
            refreshButton.DOKill();
            refreshButton.transform.rotation = Quaternion.Euler(Vector3.zero);
            refreshButton.transform.DORotate(Vector3.forward * 360f, 0.5f, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutBack);
        }
    }
}