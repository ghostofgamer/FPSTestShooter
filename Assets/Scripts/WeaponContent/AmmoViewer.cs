using TMPro;
using UnityEngine;

namespace WeaponContent
{
    public class AmmoViewer : MonoBehaviour
    {
        [SerializeField] private Weapon _currentWeapon;
        [SerializeField] private TMP_Text _ammoText;

        private void OnEnable()
        {
            _currentWeapon.AmmoChanged += UpdateAmmoText;
        }

        private void OnDisable()
        {
            _currentWeapon.AmmoChanged -= UpdateAmmoText;
        }

        private void UpdateAmmoText(int currentAmmo)
        {
            _ammoText.text = $"Ammo: {currentAmmo}/\u221e";
        }
    }
}