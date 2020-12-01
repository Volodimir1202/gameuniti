using UnityEngine;
using System.Collections.Generic;

namespace Korovetskyi_Lab1{
    public class WorldParticlesPlayer : Controller{
        [SerializeField] private List<ParticleSystem> _enemiesDeathList = null;

        public override void OnSubscribeEvents(){
            GameplayEvents.EnemyKilled += OnEnemyKilled;
        }

        public override void OnUnsubscribeEvents(){
            GameplayEvents.EnemyKilled -= OnEnemyKilled;
        }


        private void OnEnemyKilled(Vector3 pos){
            PerformEffectFromPool(_enemiesDeathList, pos);
        }

        public void PerformEffectFromPool(List<ParticleSystem> efftcs,
                                          Vector3              position,
                                          Quaternion           rotation = new Quaternion()){
            foreach (ParticleSystem effect in efftcs){
                if (!effect.isPlaying){
                    effect.transform.position = position;
                    effect.transform.rotation = rotation;
                    effect.Play();
                    return;
                }
            }
        }
    }
}