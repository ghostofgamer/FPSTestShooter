using Interfaces;
using UnityEngine;

public class HitPosition : MonoBehaviour
{
    [SerializeField, Range(0f, 5f)] private float _damageMultiplier = 1f;
    [SerializeField] private RagdollHandler _ragdollHandler;

    private IDamageable _damageable;
    private int _finalDamage;
    private int _forceFactor = 3;

    private void Start()
    {
        _damageable = GetComponentInParent<IDamageable>();
    }

    public void TakeDamage(int amount, Vector3 force, Vector3 hitPoint)
    {
        _finalDamage = Mathf.RoundToInt(amount * _damageMultiplier);
        _damageable.TakeDamage(_finalDamage, force, hitPoint);

        if (_ragdollHandler != null)
            _ragdollHandler?.Hit(-force * _forceFactor, hitPoint);
    }
}