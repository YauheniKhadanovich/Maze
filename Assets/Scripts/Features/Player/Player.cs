using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Player
{
    public class Player : MonoBehaviour
    {
        private MazeControls _controls;
        private Vector3 _nextPosition;

        private void Awake()
        {
            _controls = new MazeControls();
            _controls.MazeMap.Movements.performed += OmMovementsPerformed;
            _controls.MazeMap.Movements.canceled += OmMovementsCanceled;
            _nextPosition = transform.position;
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private void Update()
        {
            transform.position = _nextPosition;
        }

        private void OmMovementsCanceled(InputAction.CallbackContext context)
        {
            _nextPosition = transform.position;
        }

        private void OmMovementsPerformed(InputAction.CallbackContext context)
        {
            var v = context.ReadValue<Vector2>();
            _nextPosition = transform.position + new Vector3(v.x, 0, v.y);
        }
    }
}