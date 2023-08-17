using Cinemachine;
using UnityEngine;

namespace Features.CameraManagement.Impl
{
    public class CameraManager : MonoBehaviour, ICameraManager
    {
        [SerializeField] 
        private CinemachineVirtualCamera _noPlayCamera;
        [SerializeField] 
        private CinemachineVirtualCamera _playerCamera;

        public void SetCameraTarget(Transform cameraTarget)
        {
            _playerCamera.Follow = cameraTarget;
            _playerCamera.LookAt = cameraTarget;
        }

        public void PlayerCameraSetEnable(bool isEnable)
        {
            _playerCamera.gameObject.SetActive(isEnable);
        }

        public void NoPlayCameraSetEnable(bool isEnable)
        {
            _noPlayCamera.gameObject.SetActive(isEnable);
        }
    }
}