using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
<<<<<<< HEAD
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
=======
        [Header("Objects")] 
        [SerializeField] private Transform meshTransform;

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
            GameObject bulletInstance = Instantiate(currentWeapon.laser, meshTransform.position, Quaternion.identity);
            bulletInstance.transform.forward = meshTransform.forward;

            currentAmmo--;
            _fireTime = 0f;

            canShoot = false;
            StartCoroutine(SetCanShoot(currentWeapon.fireRate));
        }

        private IEnumerator SetCanShoot(float time)
        {
            yield return new WaitForSeconds(time);

            canShoot = true;
>>>>>>> main
        }

        public void SetCurrentWeapon(GunInfo gun)
        {
            currentWeapon = gun;
            currentAmmo = gun.ammo;
        }

        public float GetPlayerFireFill()
        {
            return _fireTime / currentWeapon.fireRate;
        }
    }
}
