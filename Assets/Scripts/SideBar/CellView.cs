using System;
using CommonUI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SideBar
{
    public class CellView : ViewUi, ICellView
    {
        [SerializeField] private Image backCell;
        [SerializeField] private Image icon;
        [SerializeField] private Image lockIcon;
        
        public Image Icon => icon;

        public void ShowIcon(Sprite sprite)
        {
            icon.sprite = sprite;
            icon.SetAlpha(1f);
            icon.transform.localScale = Vector3.zero;
            icon.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
            backCell.SetAlpha(0f);
        }

        public void SetOpen()
        {
            lockIcon.SetAlpha(0f);
        }
    }
}