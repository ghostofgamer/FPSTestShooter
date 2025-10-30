using UnityEngine;

namespace WeaponContent
{
    public class WeaponAnimator : MonoBehaviour
    {
        private const string Fire = "Fire";
        private const string MouseMoveAmount = "MouseMoveAmount";
        private const string Reload = "Reload";

        [SerializeField] private Animator _animator;
        [SerializeField] private int _defaultLayer = -1;

        public void PlayWeaponAnimation() => _animator.Play(Fire, _defaultLayer, 0f);

        public void PlayMoveIdleAnimation(float value) => _animator.SetFloat(MouseMoveAmount, value);

        public void PlayReload() => _animator.Play(Reload, _defaultLayer, 0f);
    }
}