using CommonUI;
using UnityEngine;
using UnityEngine.UI;

namespace SelectBoosterPopup.BoosterFly
{
    public class BoosterFlyViewUi : ViewUi
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image image;

        public RectTransform RectTransform => rectTransform;
        public Image Image => image;
    }
}