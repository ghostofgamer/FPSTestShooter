using System.Collections;
using HealthContent;
using Interfaces;
using UnityEngine;

public class HitPosition : MonoBehaviour, IDamageable
{
    [SerializeField] private Health _health;
    [SerializeField, Range(0f, 5f)] private float _damageMultiplier = 1f;
    [SerializeField] private RagdollHandler _ragdollHandler;

    /*public void TakeDamage(int amount)
    {
        int finalDamage = Mathf.RoundToInt(amount * _damageMultiplier);
        _health.DecreaseHealth(finalDamage);
    }*/

    public void TakeDamage(int amount, Vector3 force, Vector3 hitPoint)
    {
        int finalDamage = Mathf.RoundToInt(amount * _damageMultiplier);

        /*if (finalDamage >= _health.CurrentHealth)
        {
            Debug.Log("finalDamage >= _health.CurrentHealth" + finalDamage + "ваыываыва" + _health.CurrentHealth);
            StartCoroutine(asdadad(-force * 3, hitPoint));
        }*/

        _health.DecreaseHealth(finalDamage);
        _ragdollHandler?.Hit(-force* 3, hitPoint);
    }

    private IEnumerator asdadad(Vector3 force, Vector3 hitPoint)
    {
        yield return new WaitForSeconds(0.3f);
        _ragdollHandler?.Hit(force, hitPoint);
    }
}