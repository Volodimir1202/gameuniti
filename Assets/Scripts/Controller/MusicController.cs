using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Korovetskyi_Lab1{
    public sealed class MusicController : Controller{
        [SerializeField] private AnimationCurve     _fadeCurve          = null;
        [SerializeField] private AudioSource        _mainSourceLoop     = null;
        [SerializeField] private AudioSource        _sourceSingle       = null;
        [SerializeField] private AudioLowPassFilter _audioLowPassFilter = null;
        [SerializeField] private List<AudioClip>    _menuMusicList      = null;
        [SerializeField] private List<AudioClip>    _levelMusicList     = null;
        [SerializeField] private List<AudioClip>    _winMusic           = null;
        [SerializeField] private List<AudioClip>    _loseMusic          = null;
        [SerializeField] private AnimationCurve     _audioFilterCurve   = null;

        private IEnumerator _fadeCourutine = null;

        public override void OnInit(){
            PlayRandomMenuAudio();
        }

        public override void OnSubscribeEvents(){
            GameEvents.LoadLevel      += OnLevelLoad;
            GameEvents.GameOver       += OnLose;
            GameEvents.EndGameSession += PlayRandomMenuAudio;
        }

        public override void OnUnsubscribeEvents(){
            GameEvents.LoadLevel      -= OnLevelLoad;
            GameEvents.GameOver       -= OnLose;
            GameEvents.EndGameSession -= PlayRandomMenuAudio;
        }

        private void OnPlayerDamaged(){
            IEnumerator lerpCourutine = null;
            float       timeIn        = 1f;
            _audioLowPassFilter.cutoffFrequency = 1f;
            //5007.7 default
            lerpCourutine = LerpLowPassFiler(5007.7f, timeIn, _audioFilterCurve,
                                             () => { StopCoroutine(lerpCourutine); });
            StartCoroutine(lerpCourutine);
        }

        private void LevelPassed(int wave){
            _fadeCourutine = AudioTransition(_mainSourceLoop, 2f, 0.5f, 0.5f,
                                             () => { PlayRandomFromList(_winMusic, _sourceSingle); }, () => { },
                                             () => { StopCoroutine(_fadeCourutine); });
            StartCoroutine(_fadeCourutine);
        }

        private void OnLevelLoad(int lvIndex){
            StartLevelRandAudio();
        }

        private void PlayRandomMenuAudio(){
            _fadeCourutine = AudioTransition(_mainSourceLoop, 0f, 0.5f, 0.5f,
                                             () => { PlayRandomFromList(_menuMusicList, _mainSourceLoop); }, () => { },
                                             () => { StopCoroutine(_fadeCourutine); });
            StartCoroutine(_fadeCourutine);
        }

        private void StartLevelRandAudio(){
            _fadeCourutine = AudioTransition(_mainSourceLoop, 0f, 0.5f, 0.5f,
                                             () => { PlayRandomFromList(_levelMusicList, _mainSourceLoop); }, () => { },
                                             () => { StopCoroutine(_fadeCourutine); });
            StartCoroutine(_fadeCourutine);
        }

        private void OnLose(){
            _fadeCourutine = AudioTransition(_mainSourceLoop, 2f, 0.5f, 0.5f,
                                             () => { PlayRandomFromList(_loseMusic, _sourceSingle); }, () => { },
                                             () => { StopCoroutine(_fadeCourutine); });
            StartCoroutine(_fadeCourutine);
        }

        private void PlayRandomFromList(List<AudioClip> list, AudioSource source){
            source.clip = list[Random.Range(0, list.Count)];
            source.Play();
        }

        private void PlayConcrete(AudioClip clip, AudioSource source){
            source.clip = clip;
            source.Play();
        }

        public IEnumerator LerpLowPassFiler(float vValue, float time, AnimationCurve curve, Action Callback){
            float timer = 0;
            while (timer <= time){
                timer += Time.deltaTime;
                float lVal = curve.Evaluate(timer / time);
                _audioLowPassFilter.cutoffFrequency = Mathf.Lerp(_audioLowPassFilter.cutoffFrequency, vValue, lVal);
                yield return null;
            }

            Callback();
        }

        private IEnumerator AudioTransition(AudioSource source,
                                            float       pause,
                                            float       inTime,
                                            float       outTime,
                                            Action      BeforePause,
                                            Action      AfterPause,
                                            Action      Callback){
            float inTimer  = 0;
            float outTimer = 0;
            while (inTimer < inTime){
                yield return null;
                inTimer       += Time.deltaTime;
                source.volume =  _fadeCurve.Evaluate(1f - inTimer / inTime);
            }

            yield return null;
            BeforePause();
            yield return new WaitForSeconds(pause);
            while (outTimer < outTime){
                yield return null;
                outTimer += Time.deltaTime;
                //audio in
                source.volume = _fadeCurve.Evaluate(outTimer / outTime);
            }

            yield return null;
            AfterPause();
            yield return null;
            Callback();
        }
    }
}