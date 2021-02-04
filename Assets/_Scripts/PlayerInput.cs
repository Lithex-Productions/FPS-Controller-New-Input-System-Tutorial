using UnityEngine;
using UnityEngine.InputSystem;

namespace LP.FPSControllerNewInputTutorial
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private InputAction movement;
        [SerializeField] private InputAction turning;

        private CharacterController controller = null;

        private Camera cam = null;

        private float camRot = 0;

        public float speed = 5;
        public float mouseSensitivity = 100;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            cam = Camera.main;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            movement.Enable();
            turning.Enable();
        }

        private void OnDisable()
        {
            movement.Disable();
            turning.Disable();
        }
        private void Update()
        {
            Move();
            Turn();
        }

        private void Move()
        {
            float x = movement.ReadValue<Vector2>().x;
            float z = movement.ReadValue<Vector2>().y;

            Vector3 direction = transform.right * x + transform.forward * z;

            controller.Move(direction * speed * Time.deltaTime);
        }
        
        private void Turn()
        {
            float mouseX = turning.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
            float mouseY = turning.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;

            camRot -= mouseY;
            camRot = Mathf.Clamp(camRot, -90, 90);

            cam.transform.localRotation = Quaternion.Euler(camRot, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}

