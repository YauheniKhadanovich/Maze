using UnityEngine;

namespace Features.Coin.Impl
{
    public class CoinItem : MonoBehaviour
    {
        [SerializeField] 
        private Transform _coinTransform;
        private void Update()
        {
            _coinTransform.Rotate(Vector3.right * Time.deltaTime * 3f);
        }
    }
}