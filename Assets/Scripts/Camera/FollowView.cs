using UnityEngine;

namespace Korovetskyi_Lab1{
    public class FollowView : MonoBehaviour{
        [SerializeField] private Transform _root   = null;
        [SerializeField] private Transform _lookAt = null;
        [SerializeField] private Transform _stayAt = null;


        public Transform root   => _root;
        public Transform lookAt => _lookAt;
        public Transform stayAt => _stayAt;
    }
}