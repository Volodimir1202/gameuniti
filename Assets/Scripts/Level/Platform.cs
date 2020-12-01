using System;
using System.Collections.Generic;
using UnityEngine;

namespace Korovetskyi_Lab1{
    public class Platform : MonoBehaviour{
        [SerializeField] private Collider2D      _collider = null;
        [SerializeField] private List<Animation> _tiles    = null;

        private void Fall(){
            foreach (var tile in _tiles){
                tile.Play();
            }
        }

        private void OnCollisionEnter2D(Collision2D other){
            if (other.gameObject.CompareTag("Player")){
                _collider.enabled = false;
                Fall();
            }
        }
    }
}