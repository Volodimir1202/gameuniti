using UnityEngine;

namespace Korovetskyi_Lab1{
    public class PlayerWeapon : MonoBehaviour{
        [SerializeField] private Animator  _playerAnimator       = null;
        [SerializeField] private Transform _rangeWeaponPivot     = null;
        [SerializeField] private Transform _meleeWeaponPivot     = null;
        [SerializeField] private Transform _rangeWeaponContainer = null;
        [SerializeField] private Transform _meleeWeaponContainer = null;

        private MeleeWeaponBase _meleeWeapon = null;
        private RangeWeaponBase _rangeWeapon = null;

        public void AddMeleeWeapon(MeleeWeaponBase meleWeapon){
            _meleeWeapon = Instantiate(meleWeapon, Vector3.zero, Quaternion.identity, _meleeWeaponContainer);
            _meleeWeapon.transform.localPosition = Vector3.zero;
        }

        public void AddRangeWeapon(RangeWeaponBase rangeWeapon){
            _rangeWeapon = Instantiate(rangeWeapon, Vector3.zero, Quaternion.identity, _rangeWeaponContainer);
            _rangeWeapon.transform.localPosition = Vector3.zero;
        }

        public void UpdateFocus(Vector3 point){
            AlignRangeWeapon(point);
            AlignMeleeWeapon(point);
            _meleeWeapon.OnIdle();
            _rangeWeapon.OnIdle();
        }

        public void MeleeAttack(){
            _playerAnimator.SetTrigger("Hit");
            _meleeWeapon.OnAttack();
        }

        public void RangeAttack(){
            _playerAnimator.SetTrigger("Shoot");
            _rangeWeapon.OnAttack();
        }

        private void AlignRangeWeapon(Vector3 point){
            //вычисляем направление фокуса относительно позиции оружия дальнего боя
            Vector3 dir = point - Camera.main.WorldToScreenPoint(_rangeWeaponPivot.position);
            //вычисляем угол для поворота
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _rangeWeaponPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (Vector3.Dot(_rangeWeaponPivot.up, Vector3.up) > 0){
                _rangeWeaponPivot.localScale = Vector3.one;
            } else{
                _rangeWeaponPivot.localScale = new Vector3(1, -1, 1);
            }
        }

        private void AlignMeleeWeapon(Vector3 point){
            //вычисляем направление фокуса относительно позиции оружия ближнего боя
            Vector3 dir = point - Camera.main.WorldToScreenPoint(_meleeWeaponPivot.position);
            //смотрим направлен ли фокус вперед(вправно на скрине)
            if (Vector3.Dot(dir, Vector3.right) > 0){
                _meleeWeaponPivot.localPosition = new Vector3(1f, 1.5f, 0);
                _meleeWeaponPivot.localScale    = Vector3.one;
            } else{
                _meleeWeaponPivot.localPosition = new Vector3(2.5f, 1.5f, 0);
                _meleeWeaponPivot.localScale =
                    new Vector3(-1, 1, 1); // в этом случае -1 по Х самый простой способ чтобы можно было атаковать в другую сторону
            }
        }
    }
}