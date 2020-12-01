using System;
using UnityEngine;

namespace Korovetskyi_Lab1{
    public class PlayerCollision : MonoBehaviour{
        private bool _immune = false;

        private void OnTriggerEnter2D(Collider2D other){
            if (other.CompareTag("Coin")){
                GameplayEvents.CoinCollected();
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Exit")){
                GameEvents.LevelPassed();
            }

            if (other.CompareTag("Heart")){
                GameplayEvents.HeartCollected();
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Death")){
                if (_immune) return;
                GameplayEvents.Damaged(3);
            }
        }

        private void OnCollisionEnter2D(Collision2D other){
            if (_immune) return;
            if (other.gameObject.CompareTag("Damage") || other.gameObject.CompareTag("Enemy")){
                _immune = true;
                GameplayEvents.Damaged(1);
            }
        }

        public bool immune{
            get{
                return _immune;
            }
            set{
                _immune = value;
            }
        }
    }
}