using System;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace SelectBoosterPopup
{
    public class ConfirmButtonView : MonoBehaviour
    {
        private static readonly int Pressed = Animator.StringToHash("Pressed");
        
        [SerializeField] private Button button;
        [SerializeField] private Animator animator;

        private Action _callback;
        public Button Button => button;

        private void Awake()
        {
            animator.enabled = false;
        }

        internal void PlayShowAnimation()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic);
        }

        internal void PlayTapAnimation(Action callback)
        {
            animator.enabled = true;
            _callback = callback;
            animator.SetTrigger(Pressed);
        }
        
        [UsedImplicitly]
        private void OnCompleteAnimation() //call from animation event
        {
            _callback?.Invoke();
        }
    }
}