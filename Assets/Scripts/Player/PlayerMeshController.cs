using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        [Header("Objects")]
        [SerializeField] private Camera camera;
        [SerializeField] private Transform tiltSource, moveForwardSource, roombaMesh;
        [SerializeField] private Transform bulletOrigin, bulletSource;

        [Header("Fields")] 
        [SerializeField] private float rJoyLerpSpeed;
        [SerializeField] private float turnDampTime;
        [SerializeField] private float tiltLerpSpeed;
        [SerializeField] [Range(0, 90)] private float tiltDegrees;
        [SerializeField] private Material laserMaterial;
        [SerializeField] private Gradient laserColor;
        [SerializeField] private AnimationCurve laserCurve;

        [Header("Values")]
        [SerializeField] private Vector2 prevDir;
        private Vector3 _vecDamp, _fDamp;
        private bool _doMouseCheck;
        private Vector2 _prevMouse;
        private LineRenderer _lookLaser;
        private Vector2 _look;

        private void Start()
        {
            _lookLaser = gameObject.AddComponent<LineRenderer>();
            _lookLaser.material = laserMaterial;
            _lookLaser.colorGradient = laserColor;
            _lookLaser.widthCurve = laserCurve;
            _lookLaser.shadowCastingMode = ShadowCastingMode.Off;
        }

        private void Update()
        {
            Vector2 move = PlayerInputController.Instance.GetMove();
            Vector2 lookRaw = PlayerInputController.Instance.GetRJoystick();
            _look = Vector2.Lerp(_look, lookRaw, rJoyLerpSpeed * Time.deltaTime);
            Vector2 mouse = PlayerInputController.Instance.GetMousePosition();

            #region Player Looks Forward
            
            Vector2 finalLook = Vector2.zero;
            
            if (!lookRaw.Equals(Vector2.zero))
            {
                finalLook = _look.normalized;
                
                Debug.Log("Joy");
                
                prevDir = finalLook;

                _doMouseCheck = true;
                _prevMouse = mouse;
            }
            else if (Vector2.Distance(mouse, _prevMouse) > 0.5f || !_doMouseCheck)
            {
                Ray cameraRay = camera.ScreenPointToRay(mouse);
                RaycastHit hit;

                if (Physics.Raycast(cameraRay, out hit))
                {
                    finalLook = new Vector2(hit.point.x, hit.point.z) - new Vector2(transform.position.x, transform.position.z);
                    
                    prevDir = finalLook;
                }

                _doMouseCheck = false;
            }
            else if (!move.Equals(Vector2.zero))
            {
                finalLook = move.normalized;
                prevDir = finalLook;
                
                _doMouseCheck = true;
                _prevMouse = mouse;
            }
            else
            {
                finalLook = prevDir;
            }
            
            Vector3 forward = (finalLook.normalized - Vector2.zero).normalized;
            forward.z = forward.y;
            forward.y = 0;

            transform.forward = Vector3.SmoothDamp(transform.forward, forward, ref _vecDamp, turnDampTime);
            bulletOrigin.transform.forward = forward;
            
            //Debug.DrawRay(transform.position, transform.forward, Color.green);
            
            #endregion

            #region Tilt
            
            Quaternion goal = Quaternion.Euler(new Vector3(!move.Equals(Vector2.zero) ? tiltDegrees : 0, 0, 0));
            //tiltSource.localRotation = Quaternion.Slerp(tiltSource.localRotation, goal, tiltLerpSpeed * Time.deltaTime);
            // tiltSource.up = Vector3.Slerp(tiltSource.up, new Vector3(move.x, 3f, move.y),
            //     tiltLerpSpeed * Time.deltaTime);
            moveForwardSource.forward = Vector3.SmoothDamp(moveForwardSource.forward, new Vector3(move.x, 0, move.y),
                ref _fDamp, turnDampTime);
            //roombaMesh.localRotation = Quaternion.Euler(tiltSource.localEulerAngles.x, roombaMesh.localEulerAngles.y, tiltSource.localEulerAngles.z);

            roombaMesh.localRotation = Quaternion.Slerp(roombaMesh.localRotation,
                Quaternion.Euler(new Vector3(move.x * tiltDegrees, 90, move.y * tiltDegrees)),
                tiltLerpSpeed * Time.deltaTime);

            #endregion

            #region Laser

            _lookLaser.SetPosition(0, bulletSource.position);
            _lookLaser.SetPosition(1, bulletSource.position + (bulletSource.forward * 50));
            
            RaycastHit hitLaser;
            if (Physics.Raycast(bulletSource.position, bulletSource.forward, out hitLaser, Mathf.Infinity))
            {
                if (!hitLaser.transform.CompareTag("Laser"))
                {
                    _lookLaser.SetPosition(1, hitLaser.point);
                }
            }

            #endregion
        }
    }
}