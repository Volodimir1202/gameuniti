using UnityEngine;

namespace Korovetskyi_Lab1{
    public class Gun : RangeWeaponBase{
        [SerializeField] private AnimatedTexture _muzzleFlesh = null;
        private                  float           _timer       = 0;
        private                  float           _time        = 0.1f;

        public override void OnAttack(){
            _muzzleFlesh.transform.localScale = Vector3.one * 0.5f;

            _timer = _time;
            _muzzleFlesh.NextFrame();

            RaycastHit2D hit =
                Physics2D.Raycast(gameObject.transform.position + transform.right * 2, transform.right, 100);
            if (hit.transform != null){
                if (hit.collider.CompareTag("Enemy")){
                    GameplayEvents.EnemyKilled(hit.transform.position);
                    Destroy(hit.transform.gameObject);
                }
            }
        }

        public override void OnIdle(){
            _timer                            -= Time.deltaTime;
            _timer                            =  Mathf.Clamp(_timer, 0, _time);
            _muzzleFlesh.transform.localScale =  Vector3.one * _timer / _time * 0.5f;
        }
    }
}