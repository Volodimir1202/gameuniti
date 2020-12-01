using System;
using UnityEngine;

namespace Korovetskyi_Lab1{
    public class Sword : MeleeWeaponBase{
        [SerializeField] private TrailRenderer _trail;

        private float _delay = 0;

        public override void OnAttack(){
            _delay         = 0;
            _trail.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D other){
            if (other.CompareTag("Enemy")){
                GameplayEvents.EnemyKilled(other.transform.position);
                Destroy(other.gameObject);
            }
        }

        public override void OnIdle(){
            _delay += Time.deltaTime;
            if (_delay > 0.2f)
                _trail.enabled = false;
        }
    }
}