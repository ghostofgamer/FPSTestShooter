using UnityEngine;

namespace WeaponContent
{
    public class WeaponAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void PlayWeaponAnimation()
        {
            _animator.Play("Fire", -1, 0f);
        }

        public void PlayMoveIdleAnimation(float value)
        {
            _animator.SetFloat("MouseMoveAmount", value);
        }
    }
}