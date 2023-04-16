using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    [CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Gun")]
    public class GunInfo : ScriptableObject
    {
        [Header("Gun Info")]
        public GunName gunName;

        public Mesh pickupMesh;
        public GameObject laser;
        
        [Header("Fire Mod")]
        public float fireRate;
        public bool fullAuto;
        public int ammo;
        
        [Header("Shot Spread")]
        public bool doSpread;
        public int spreadCount;
        public float spreadAngle;

        public enum GunName
        {
            Sniper,
            Pistol,
            Assault,
            Shotgun,
            Random
        }

        public void RandomizeWeapon()
        {
            float fireRateMin = 0.05f, fireRateMax = 4f;
            int randomAmmoMin = 1, randomAmmoMax = 50;

            int spreadCountMin = 1, spreadCountMax = 16;
            float spreadAngleMin = 15, spreadAngleMax = 345;

            bool randomDoSpread = Random.Range(0, 1) == 0;
            bool fullAutoRandom = Random.Range(0, 1) == 0;

            float tempFireRate = Random.Range(fireRateMin, fireRateMax);
            int randomAmmo = Random.Range(randomAmmoMin, randomAmmoMax);

            int tempSpreadCount = Random.Range(spreadCountMin, spreadCountMax);
            float spreadAngleTemp = Random.Range(spreadAngleMin, spreadAngleMax);

            fireRate = tempFireRate;
            ammo = randomAmmo;
            spreadCount = tempSpreadCount;
            spreadAngle = spreadAngleTemp;

            doSpread = randomDoSpread;
            fullAuto = fullAutoRandom;
        }
    }
}
