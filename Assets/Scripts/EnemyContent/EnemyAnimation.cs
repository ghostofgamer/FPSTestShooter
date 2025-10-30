using System;
using UnityEngine;

namespace EnemyContent
{
    public class EnemyAnimation : MonoBehaviour
    {
        private const string WalkAnimation = "Walk";
        private const string AttackAnimation = "Attack";

        [SerializeField] private Animator _animator;

        private int _layer = -1;
        private float _normalizedTime = 0f;

        public event Action AttackHitEvent;

        public void EnableAnimator() => _animator.enabled = true;

        public void DisableAnimator() => _animator.enabled = false;

        public void PlayWalk(bool value) => _animator.SetBool(WalkAnimation, value);

        public void PlayAttack() => _animator.Play(AttackAnimation, _layer, _normalizedTime);

        public void OnAttackHit() => AttackHitEvent?.Invoke();
    }
}