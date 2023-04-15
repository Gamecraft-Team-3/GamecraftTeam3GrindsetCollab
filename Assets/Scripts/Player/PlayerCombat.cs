using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        // [Header("Components")] 
        // [SerializeField] private PlayerMeshController meshController;
        
        [Header("Objects")] 
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform meshTransform;

        [Header("Values")] 
        [SerializeField] private bool canShoot;
        [SerializeField] private bool isShootPressed;
        
        [Header("Fields")]
        [SerializeField] private float fireRate;
        [SerializeField] private bool fullAuto;

        private void Start()
        {
            PlayerInputController.Instance.OnShootActionPerformed += ShootPressed;
            PlayerInputController.Instance.OnShootActionReleased += ShootReleased;
        }

        private void Update()
        {
            if (fullAuto && canShoot && isShootPressed)
                Shoot();
        }

        private void ShootPressed(object sender, EventArgs e)
        {
            isShootPressed = true;

            if (!fullAuto)
                Shoot();
        }

        private void ShootReleased(object sender, EventArgs e)
        {
            isShootPressed = false;
        }

        private void Shoot()
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, meshTransform.position, Quaternion.identity);
            bulletInstance.transform.forward = meshTransform.forward;

            canShoot = false;
            StartCoroutine(SetCanShoot(fireRate));
        }

        private IEnumerator SetCanShoot(float time)
        {
            yield return new WaitForSeconds(time);

            canShoot = true;
        }
    }
}
