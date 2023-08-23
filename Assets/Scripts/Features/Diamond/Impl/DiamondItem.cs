using UnityEngine;

namespace Features.Diamond.Impl
{
    public class DiamondItem : MonoBehaviour
    {
        [SerializeField] 
        private Transform _diamondTransform;
        
        private void Update()
        {
            _diamondTransform.Rotate(Vector3.up * Time.deltaTime * 70f);
        }
    }
}