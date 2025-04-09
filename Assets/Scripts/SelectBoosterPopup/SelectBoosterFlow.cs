using System.Collections;
using DG.Tweening;
using Particles;
using SideBar;
using UnityEngine;
using UpperPanel;

namespace SelectBoosterPopup
{
    public class SelectBoosterFlow
    {
        private readonly ICellHolder _cellHolder;
        private readonly ISideBar _sideBar;
        private readonly IParticlePlayer _particlePlayer;
        private readonly IUpperPanel _upperPanel;

        public SelectBoosterFlow(ICellHolder cellHolder, ISideBar sideBar, IParticlePlayer particlePlayer,
            IUpperPanel upperPanel)
        {
            _cellHolder = cellHolder;
            _sideBar = sideBar;
            _particlePlayer = particlePlayer;
            _upperPanel = upperPanel;
        }

        internal IEnumerator SelectedBoosterFlow(SelectBoosterView selectedBoosterView)
        {
            yield return new WaitForSeconds(0.3f);

            var moveTween = selectedBoosterView.Root.DOAnchorPos(Vector2.zero, 0.5f);
            var scaleTween = selectedBoosterView.Root.DOScale(1.2f, 0.5f);

            yield return DOTween.Sequence() //shift to center
                .Join(moveTween)
                .Join(scaleTween)
                .WaitForCompletion();

            selectedBoosterView.SetPicked(true); //show checkmark

            var starsParticle = _particlePlayer.PlayStarsParticle(); //show particles

            if (starsParticle.IsAlive)
                yield return new WaitUntil(() => starsParticle.IsAlive == false); //wait finish particles

            if (_sideBar.IsVisible == false) 
                _sideBar.SetVisible(true); //show side bar

            var cell = _cellHolder.GetFirstAvailableCell();

            cell.Icon.SetAlpha(1f);
            cell.Icon.sprite = selectedBoosterView.Icon.sprite;
            cell.Icon.rectTransform.sizeDelta = selectedBoosterView.Root.sizeDelta;
            cell.Icon.transform.position = selectedBoosterView.transform.position;
            selectedBoosterView.gameObject.SetActive(false);
            cell.PlayFly();

            yield return new WaitForSeconds(1f);

            _sideBar.SetVisible(false);
            _upperPanel.Hide();
        }
    }
}