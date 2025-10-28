using Interfaces;
using UnityEngine;

namespace HealthContent
{
    public class Health : MonoBehaviour,IDamageable
    {
        public void TakeDamage(int amount)
        {
            Debug.Log("Попадание!!!");
        }
    }
}