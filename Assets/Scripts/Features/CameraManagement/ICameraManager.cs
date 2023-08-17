using UnityEngine;

namespace Features.CameraManagement
{
    public interface ICameraManager
    {
        void SetCameraTarget(Transform cameraTarget);
        void PlayerCameraSetEnable(bool isEnable);
        void NoPlayCameraSetEnable(bool isEnable);
    }
}