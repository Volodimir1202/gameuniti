using System.Collections.Generic;
using UnityEngine;

namespace Korovetskyi_Lab1{
    public sealed class SFXController : Controller{
        [SerializeField] private List<AudioSource> _mainSources         = null;
        [SerializeField] private List<AudioClip>   _rangeAttackSounds   = null;
        [SerializeField] private List<AudioClip>   _meleeAttackSounds   = null;
        [SerializeField] private List<AudioClip>   _meleeHitSounds      = null;
        [SerializeField] private List<AudioClip>   _playerDamaged       = null;

        public override void OnInit(){
            // SetSoundState(PrefsData.Settings.soundEnabled);
        }

        public override void OnSubscribeEvents(){
            GameplayEvents.RangeAttack += OnRngeAttack;
            GameplayEvents.MeleeAttack += MeleeAttack;
            GameplayEvents.EnemyKilled += OnHit;
            GameplayEvents.Damaged     += PlayerDamaged;
        }

        public override void OnUnsubscribeEvents(){
            GameplayEvents.RangeAttack -= OnRngeAttack;
            GameplayEvents.MeleeAttack -= MeleeAttack;
            GameplayEvents.EnemyKilled -= OnHit;
            GameplayEvents.Damaged     -= PlayerDamaged;
        }

        private void PlayerDamaged(int amount){
            PlayRandomFromList(_playerDamaged, GetReadySource(_mainSources));
        }

        private void OnRngeAttack(){
            PlayRandomFromList(_rangeAttackSounds, GetReadySource(_mainSources), true);
        }

        private void OnHit(Vector3 pos){
            PlayRandomFromList(_meleeHitSounds, GetReadySource(_mainSources), true);
        }

        private void MeleeAttack(){
            PlayRandomFromList(_meleeAttackSounds, GetReadySource(_mainSources), true);
        }

        private void SetSoundState(bool value){
            // PrefsData.Settings.soundEnabled = value;

            for (int i = 0; i < _mainSources.Count; i++){
                _mainSources[i].mute = !value;
            }

            // PlayRandomFromList(_objectDisappearSound, GetReadySource(_mainSources));
        }

        private void PlayRandomFromList(List<AudioClip> list, AudioSource source, bool randPitch = false){
            if (randPitch){
                source.pitch = Random.Range(0.8f, 1.2f);
            } else{
                source.pitch = 1f;
            }

            source.clip = list[Random.Range(0, list.Count)];
            source.Play();
        }

        private void PlayConcreteFromList(int i, List<AudioClip> list, AudioSource source){
            source.clip = list[i];
            source.Play();
        }

        private AudioSource GetReadySource(List<AudioSource> list){
            for (int i = 0; i < list.Count; i++){
                if (!list[i].isPlaying){
                    return list[i];
                }
            }

            return list[0];
        }
    }
}