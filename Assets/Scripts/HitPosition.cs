using Interfaces;
using UnityEngine;

public class HitPosition : MonoBehaviour
{
    [SerializeField, Range(0f, 5f)] private float _damageMultiplier = 1f;
    [SerializeField] private RagdollHandler _ragdollHandler;

    private IDamageable _damageable;

    private void Start()
    {
        _damageable = GetComponentInParent<IDamageable>();

        if (_damageable == null)
        {
            Debug.LogError(
                $"[HitPosition] У объекта {name} не найден компонент, реализующий IDamageable, среди родителей!");
        }
        else
        {
            Debug.Log("_damageable.GetType().Name " + _damageable.GetType().Name);
        }
    }

    public void TakeDamage(int amount, Vector3 force, Vector3 hitPoint)
    {
        int finalDamage = Mathf.RoundToInt(amount * _damageMultiplier);
        _damageable.TakeDamage(finalDamage, force, hitPoint);
        
        if (_ragdollHandler != null)
            _ragdollHandler?.Hit(-force * 3, hitPoint);
    }
}