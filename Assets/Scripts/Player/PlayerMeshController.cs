using System;
using UnityEngine;

namespace Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        [Header("Objects")]
        [SerializeField] private Camera camera;
        [SerializeField] private Transform tiltSource, moveForwardSource;

        [Header("Fields")]
        [SerializeField] private float turnDampTime;
        [SerializeField] private float tiltLerpSpeed;
        [SerializeField] [Range(0, 90)] private float tiltDegrees;

        [Header("Values")]
        [SerializeField] private Vector2 prevDir;
        private Vector3 _vecDamp, _fDamp;
        private bool _doMouseCheck;
        private Vector2 _prevMouse;
        private Vector2 _smoothMove;
        
        private void Update()
        {
            Vector2 move = PlayerInputController.Instance.GetMove();
            Vector2 look = PlayerInputController.Instance.GetRJoystick();
            Vector2 mouse = PlayerInputController.Instance.GetMousePosition();

            #region Player Looks Forward
            
            
            Vector2 finalLook = Vector2.zero;

            if (!look.Equals(Vector2.zero))
            {
                finalLook = look.normalized;
                
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
            
            Debug.DrawRay(transform.position, transform.forward, Color.green);
            
            #endregion

            #region Tilt
            
            Quaternion goal = Quaternion.Euler(new Vector3(!move.Equals(Vector2.zero) ? tiltDegrees : 0, 0, 0));
            tiltSource.localRotation = Quaternion.Slerp(tiltSource.localRotation, goal, tiltLerpSpeed * Time.deltaTime);
            moveForwardSource.forward = Vector3.SmoothDamp(moveForwardSource.forward, new Vector3(move.x, 0, move.y),
                ref _fDamp, turnDampTime);

            #endregion
        }
    }
}