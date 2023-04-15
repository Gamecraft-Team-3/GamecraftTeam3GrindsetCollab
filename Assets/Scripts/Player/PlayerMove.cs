using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMove : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody rb;

        [Header("Values")]
        [SerializeField] private Vector2 moveInput;
        [SerializeField] private Vector3 move;

        [Header("Fields")] 
        [SerializeField] private float playerSpeed;
        [SerializeField] private float playerMoveLerp;
        [SerializeField] private float playerVelocityLerp;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            moveInput = PlayerInputController.Instance.GetMove().normalized;
        
            Vector3 moveTemp = moveInput * (playerSpeed * Time.deltaTime);
            move = Vector3.Lerp(move, moveTemp, playerMoveLerp * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            Vector3 tempVelocity = new Vector3(move.x, 0, move.y) * 500;
            rb.velocity = Vector3.Lerp(rb.velocity, tempVelocity, playerVelocityLerp * Time.fixedDeltaTime);
        }
    }
}