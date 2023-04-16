using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [Header("Objects")] 
        [SerializeField] private Transform meshTransform;
        [SerializeField] private AmmoScript ammoScript;

        [Header("Values")] 
        [SerializeField] private bool canShoot;
        [SerializeField] private bool isShootPressed;
        [SerializeField] private GunInfo currentWeapon;
        [SerializeField] private int currentAmmo;
        private float _fireTime;

        private void Start()
        {
            PlayerInputController.Instance.OnShootActionPerformed += ShootPressed;
            PlayerInputController.Instance.OnShootActionReleased += ShootReleased;

            currentAmmo = currentWeapon.ammo;

            ammoScript.SetAmmo(currentAmmo);

        }

        private void Update()
        {
            if (_fireTime < currentWeapon.fireRate)
                _fireTime += Time.deltaTime;
            else
                _fireTime = currentWeapon.fireRate;

            if (currentWeapon.fullAuto && canShoot && isShootPressed && currentAmmo > 0)
                Shoot();
        }

        private void ShootPressed(object sender, EventArgs e)
        {
            isShootPressed = true;

            if (!currentWeapon.fullAuto && canShoot && currentAmmo > 0)
                Shoot();
        }

        private void ShootReleased(object sender, EventArgs e)
        {
            isShootPressed = false;
        }

        private void Shoot()
        {
            if (currentWeapon.doSpread)
            {
                for (int i = 1; i < currentWeapon.spreadCount + 1; i++)
                {
                    float angle = (currentWeapon.spreadAngle / 2) - (currentWeapon.spreadAngle / i);
                    
                    GameObject subBulletInstance = Instantiate(currentWeapon.laser, meshTransform.position, Quaternion.identity);
                    subBulletInstance.transform.forward = meshTransform.forward;
                    subBulletInstance.transform.Rotate(new Vector3(0, angle, 0));
                }
            }
            else
            {
                GameObject bulletInstance = Instantiate(currentWeapon.laser, meshTransform.position, Quaternion.identity);
                bulletInstance.transform.forward = meshTransform.forward;
            }

            currentAmmo--;
            ammoScript.SetAmmo(currentAmmo);
            _fireTime = 0f;

            canShoot = false;
            StartCoroutine(SetCanShoot(currentWeapon.fireRate));
        }

        private IEnumerator SetCanShoot(float time)
        {
            yield return new WaitForSeconds(time);

            canShoot = true;
        }

        public void SetCurrentWeapon(GunInfo gun)
        {
            currentWeapon = gun;
            currentAmmo = gun.ammo;
            ammoScript.SetAmmo(currentAmmo);
        }

        public float GetPlayerFireFill()
        {
            return _fireTime / currentWeapon.fireRate;
        }

        public GunInfo GetCurrentGun()
        {
            return currentWeapon;
        }

        public bool CanPlayerFire()
        {
            return canShoot;
        }
    }
}
