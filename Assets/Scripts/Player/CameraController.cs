using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        [Header("Objects")]
        [SerializeField] private Transform target;
        private Camera mainCamera;
        
        [Header("Values")]
        [SerializeField] private Vector3 offset, lookOffset;
        [SerializeField] [Range(0f, 1f)] private float lookIntensity, farLookIntensity;
        [SerializeField] private float followSpeed;
        [SerializeField] private float controllerSmoothingIntensity;
        public bool active;
        
        private Vector3 _velDamper = Vector3.zero;
        private Vector2 _controllerSmoothed;
        private Vector2 _prevMouse;
        private bool _doMouseDeltaCheck;

        private void Start()
        {
            mainCamera = GetComponent<Camera>();
            transform.parent = null;
        }

        private void Update()
        {
            if (!active)
                return;

            Vector2 controllerIn = PlayerInputController.Instance.GetRJoystick();
            Vector2 mouseIn = PlayerInputController.Instance.GetMousePosition();

            Vector2 location;

            _controllerSmoothed = Vector2.Lerp(_controllerSmoothed, controllerIn,
                controllerSmoothingIntensity * Time.deltaTime);

            if (!controllerIn.Equals(Vector2.zero))
            {
                Vector2 controllerScreenSpace = new Vector2(_controllerSmoothed.x * (Screen.width / 2), _controllerSmoothed.y * (Screen.height / 2));
                
                location = new Vector2(Screen.width / 2, Screen.height / 2) + controllerScreenSpace;
                
                Cursor.visible = false;

                _prevMouse = mouseIn;

                _doMouseDeltaCheck = true;
            }
            else
            {
                if (Vector2.Distance(mouseIn, _prevMouse) > 0.05f || !_doMouseDeltaCheck)
                {
                    location = mouseIn;
                    Cursor.visible = true;

                    _doMouseDeltaCheck = false;
                }
                else
                {
                    location = new Vector2(Screen.width / 2, Screen.height / 2);
                }
            }


            Ray cameraRay = mainCamera.ScreenPointToRay(location);
            RaycastHit hit;

            if (Physics.Raycast(cameraRay, out hit))
            {
                Vector3 mousePos = hit.point;

                Vector3 delta = (new Vector3(target.position.x, 0 ,target.position.z) - hit.point) * -1;

                delta.y *= 1.25f;

                lookOffset = Vector3.Lerp(Vector2.zero, delta, PlayerInputController.Instance.GetLook() ? farLookIntensity : lookIntensity);
            }
        }

        private void LateUpdate()
        {
            // if (Vector2.Distance(target.position, (Vector2)target.position + 
            //                                       new Vector2(offset.x + lookOffset.x, offset.z + lookOffset.z)) < 0.5 && !_doMouseDeltaCheck)
            // {
            //     lookOffset = Vector2.zero;
            // }
            
            Vector3 position = transform.position;
            Vector3 targetPosition = new Vector3(target.position.x, 0 ,target.position.z) + new Vector3(offset.x + lookOffset.x, 0, offset.z + lookOffset.z);
            
            Vector3 finalPosition = Vector3.SmoothDamp(position, targetPosition, ref _velDamper, followSpeed);

            transform.position = new Vector3(finalPosition.x, offset.y, finalPosition.z);
        }
        
        public void SetOffset(Vector3 offset)
        {
            this.offset = offset;
        }

        public Vector3 GetOffset()
        {
            return offset;
        }
    }
}