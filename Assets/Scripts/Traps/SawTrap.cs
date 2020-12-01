using System;
using System.Collections.Generic;
using UnityEngine;

namespace Korovetskyi_Lab1{
    public class SawTrap : LevelUpdatable{
        [SerializeField] private Transform      _root        = null;
        [SerializeField] private List<Vector3>  _points      = null;
        [SerializeField] private AnimationCurve _movingCurve = null;
        [SerializeField] private float          _time        = 5f;

        private int   _pointIndex = 0;
        private float _timer      = 0;

        public override void OnTick(float dt){
            _timer += dt;
            CheckIndex();
            UpdateSaw(_timer / _time);
        }

        private void UpdateSaw(float val){
            _root.position = Vector3.Lerp(_root.position, _points[_pointIndex], _movingCurve.Evaluate(val));
        }

        private void CheckIndex(){
            if (GetDistance(_root.position, _points[_pointIndex]) < 1f){
                _pointIndex++;
                _timer = 0;
                if (_pointIndex >= _points.Count){
                    _pointIndex = 0;
                }
            }
        }

        public float GetDistance(Vector3 a, Vector3 b){
            Vector3 v = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
            return Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        }
    }
}