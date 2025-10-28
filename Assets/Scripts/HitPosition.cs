using HealthContent;
using Interfaces;
using UnityEngine;

public class HitPosition : MonoBehaviour, IDamageable
{
    [SerializeField] private Health _health;
    [SerializeField, Range(0f, 5f)] private float _damageMultiplier = 1f;
    [SerializeField] private RagdollHandler _ragdollHandler;

    public void TakeDamage(int amount, Vector3 force, Vector3 hitPoint)
    {
        int finalDamage = Mathf.RoundToInt(amount * _damageMultiplier);

        _health.DecreaseHealth(finalDamage);
        
        if (_ragdollHandler != null)
            _ragdollHandler?.Hit(-force * 3, hitPoint);
    }
}