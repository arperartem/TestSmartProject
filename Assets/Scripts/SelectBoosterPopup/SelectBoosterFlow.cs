using System.Collections;
using DG.Tweening;
using Particles;
using SelectBoosterPopup.BoosterFly;
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
        private readonly BoosterFlyManager _boosterFlyManager;
        private readonly Camera _camera;

        public SelectBoosterFlow(ICellHolder cellHolder, ISideBar sideBar, IParticlePlayer particlePlayer,
            IUpperPanel upperPanel, BoosterFlyManager boosterFlyManager, Camera camera)
        {
            _cellHolder = cellHolder;
            _sideBar = sideBar;
            _particlePlayer = particlePlayer;
            _upperPanel = upperPanel;
            _boosterFlyManager = boosterFlyManager;
            _camera = camera;
        }

        public IEnumerator SelectedBoosterFlow(SelectBoosterView selectedBoosterView)
        {
            yield return new WaitForSeconds(0.3f);

            var moveTween = selectedBoosterView.Root.DOAnchorPos(Vector2.zero, 0.5f);
            var scaleTween = selectedBoosterView.Root.DOScale(1.2f, 0.5f);

            yield return DOTween.Sequence() //shift to center
                .Join(moveTween)
                .Join(scaleTween)
                .WaitForCompletion();

            selectedBoosterView.SetPicked(true); //show checkmark

            var selectBoosterParticle = _particlePlayer.PlayParticle(ParticleType.SelectBooster,
                _camera.ScreenToWorldPoint(selectedBoosterView.Root.position)); //show particles

            if (selectBoosterParticle.IsAlive)
                yield return new WaitUntil(() => selectBoosterParticle.IsAlive == false); //wait finish particles

            if (_sideBar.IsVisible == false)
            {
                _sideBar.SetVisible(true); //show side bar
                yield return new WaitUntil(() => _sideBar.IsAnimating == false);
            }

            var cell = _cellHolder.GetFirstAvailableCell();

            selectedBoosterView.gameObject.SetActive(false);

            var completeFly = false;

            //fly booster to available side bar cell
            _boosterFlyManager.PlayBoosterFly(selectedBoosterView.Root, cell.Icon.rectTransform,
                selectedBoosterView.Icon.sprite, selectedBoosterView.transform.parent, () =>
                {
                    cell.ShowIcon(selectedBoosterView.Icon.sprite);
                    completeFly = true;
                });

            yield return new WaitUntil(() => completeFly);
            
            var cellParticle = _particlePlayer.PlayParticle(ParticleType.SideCell,
                _camera.ScreenToWorldPoint(cell.Icon.transform.position)); //show particles
            
            if (cellParticle.IsAlive)
                yield return new WaitUntil(() => cellParticle.IsAlive == false); //wait finish particles
            
            _sideBar.SetVisible(false);
            _upperPanel.Hide();
        }
    }
}