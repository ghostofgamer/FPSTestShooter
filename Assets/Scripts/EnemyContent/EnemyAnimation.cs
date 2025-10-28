using UnityEngine;

namespace EnemyContent
{
    public class EnemyAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void EnableAnimator()
        {
            _animator.enabled = true;
        }

        public void DisableAnimator()
        {
            _animator.enabled = false;
        }
    }
}