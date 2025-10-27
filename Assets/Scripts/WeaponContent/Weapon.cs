using SOContent;
using UnityEngine;

namespace WeaponContent
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponConfig _weaponConfig;

        public WeaponConfig WeaponConfig => _weaponConfig;

        public void OnHit(RaycastHit hit)
        {
            Debug.Log("hit " + hit.collider.gameObject.name);
        }
    }
}