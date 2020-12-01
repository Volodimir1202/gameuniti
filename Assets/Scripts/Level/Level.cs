using System;
using System.Collections.Generic;
using UnityEngine;

namespace Korovetskyi_Lab1{
    [Serializable]
    public class Level : MonoBehaviour{
        [SerializeField] public List<LevelUpdatable> updatables = null;

        public void Init(){
        }

        public void OnTick(float dt){
            if (GameEvents.gamePaused) return;
            UpdateUpdatable(Time.deltaTime);
        }

        private void UpdateUpdatable(float dt){
            foreach (var uptbl in updatables){
                if (uptbl != null)
                    uptbl.OnTick(dt);
            }
        }
    }
}