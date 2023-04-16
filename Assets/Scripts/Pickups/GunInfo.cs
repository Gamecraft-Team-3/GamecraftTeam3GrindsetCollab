using System;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Gun")]
    public class GunInfo : ScriptableObject
    {
        [Header("Gun Info")]
        public GunName gunName;
        public Mesh mesh;
        public GameObject laser;
        
        [Header("Fire Mod")]
        public float fireRate;
        public bool fullAuto;
        public int ammo;
        
        [Header("Reflection")]
        public int bounceCount;
        
        public enum GunName
        {
            Sniper,
            Pistol,
            Assault
        }
    }
}
