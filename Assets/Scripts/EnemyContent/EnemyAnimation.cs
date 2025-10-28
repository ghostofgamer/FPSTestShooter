using System;
using UnityEngine;

namespace EnemyContent
{
    public class EnemyAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public event Action AttackHitEvent;
        
        public void EnableAnimator()
        {
            _animator.enabled = true;
        }

        public void DisableAnimator()
        {
            _animator.enabled = false;
        }

        public void PlayWalk(bool value)
        {
            _animator.SetBool("Walk", value);
        }

        public void PlayAttack()
        {
            _animator.Play("Attack", -1, 0f);
        }
        
        public void OnAttackHit()
        {
            AttackHitEvent?.Invoke();
        }
    }
}