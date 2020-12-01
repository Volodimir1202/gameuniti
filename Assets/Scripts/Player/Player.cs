using UnityEngine;

namespace Korovetskyi_Lab1{
    public class Player : MonoBehaviour{
        [SerializeField] private PlayerMove      _move;
        [SerializeField] private PlayerWeapon    _weapon;
        [SerializeField] private PlayerVisual    _visual;
        [SerializeField] private PlayerCollision _collision;
        public                   PlayerMove      move      => _move;
        public                   PlayerWeapon    weapon    => _weapon;
        public                   PlayerVisual    visual    => _visual;
        public                   PlayerCollision collision => _collision;
    }
}