using System.Collections.Generic;
using UnityEngine;

namespace Korovetskyi_Lab1{
    public class SimpleEnemy : LevelUpdatable{
        [SerializeField] private Player        _player = null;
        [SerializeField] private List<Vector3> _points = null;

        private int _pointIndex = 0;

        public override void OnTick(float dt){
            CheckIndex();
            UpdatePosition();
        }

        private void UpdatePosition(){
            _player.move.OnMove(_points[_pointIndex] - transform.position);
            _player.visual.UpdateFocus(_points[_pointIndex], false);
        }

        private void CheckIndex(){
            if (Vector3.Distance(transform.position, _points[_pointIndex]) < 1f){
                _pointIndex++;
                if (_pointIndex >= _points.Count){
                    _pointIndex = 0;
                }
            }
        }
    }
}