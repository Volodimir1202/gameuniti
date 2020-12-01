using UnityEngine;

namespace Korovetskyi_Lab1{
    public class CameraData : MonoBehaviour{
        [SerializeField] private Transform _root   = null;
        [SerializeField] private Transform _target = null;
        [SerializeField] private Camera    _cam    = null;

        private Vector3 _offset = new Vector3();

        public void Init(){
        }

        public void UpdatePosition(float dt){
            Vector3 finalPosition = _target.position + _offset + Vector3.up * 5;
            finalPosition.y *= 0.5f;
            finalPosition.y =  Mathf.Clamp(finalPosition.y, 4, Mathf.Infinity);
            _root.position  =  Vector3.Lerp(_root.position, finalPosition, dt * 10f);
        }

        public void SetFocus(Vector3 point){
            Vector3 dir = (point - _cam.WorldToScreenPoint(_target.position)).normalized;
            dir.z   = 0;
            dir.y   = 0;
            _offset = dir * 5f;
        }

        public Camera cam => _cam;
    }
}