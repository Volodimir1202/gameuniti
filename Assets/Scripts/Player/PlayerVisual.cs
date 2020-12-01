using UnityEngine;

namespace Korovetskyi_Lab1{
    public class PlayerVisual : MonoBehaviour{
        [SerializeField] private Animator _playerAnimator = null;
        [SerializeField] private Transform _eyesPivot    = null;
        [SerializeField] private Transform _hatContainer = null;

        public void UpdateFocus(Vector3 point, bool fromCamera = true){
            //чтобы немного "оживить" картинку
            UpdateEyesPosition(point, fromCamera);
        }

        public void Damaged(){
            _playerAnimator.SetTrigger("Damaged");
        }
        private void UpdateEyesPosition(Vector3 point, bool fromCamera){
            Vector3 dir = Vector3.down;
            if (fromCamera){
                dir   = (point - Camera.main.WorldToScreenPoint(_eyesPivot.position)).normalized;
                dir.z = 0;
            } else{
                dir = (point - _eyesPivot.position).normalized;
            }

            _eyesPivot.localPosition = new Vector3(1.66f, 1.62f, 0) + dir * 0.6f;
        }

        public void SetHat(GameObject hat){
            if (_hatContainer.childCount > 0){
                Destroy(_hatContainer.GetChild(0).gameObject);
            }

            GameObject newhat = Instantiate(hat, Vector3.zero, Quaternion.identity, _hatContainer);
            newhat.transform.localPosition = Vector3.zero;
        }
    }
}