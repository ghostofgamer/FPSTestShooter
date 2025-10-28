using UnityEngine;

namespace Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(int amount, Vector3 force, Vector3 hitPoint);
    }
}