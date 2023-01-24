using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private Transform weaponSlot;
    private GameObject currentWeapon;

    [SerializeField]
    private WeaponDataSO equippedWeapon;

    [SerializeField]
    AudioSource pickUpSound;

    public void EquipWeapon(WeaponDataSO weaponData)
    {
        equippedWeapon = weaponData;
         if (currentWeapon != null)
            Destroy(currentWeapon);

        currentWeapon = Instantiate(weaponData.weaponPrefab);
        currentWeapon.transform.SetParent(weaponSlot);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }
}
