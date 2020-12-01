using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Korovetskyi_Lab1{
    public sealed class MainMenuScreen : UIScreen{
        [SerializeField] private Button  _btnContinue         = null;
        [SerializeField] private Button  _btnLevelSelection   = null;
        [SerializeField] private Button  _btnExit             = null;
        [SerializeField] private Button  _btnBackToMenu       = null;
        [SerializeField] private UIPanel _levelSelectionPanel = null;

        [SerializeField] private CanvasGroup _canvasGroupButtons        = null;
        [SerializeField] private CanvasGroup _canvasGroupLevelSelection = null;

        [Header("Toggle")] [SerializeField] private GraphicRaycaster _raycaster = null;

        [SerializeField] private Canvas       _canvas       = null;
        [SerializeField] private CanvasScaler _canvasScaler = null;
        [SerializeField] private GameObject   _contentRoot  = null;


        public override void OnInit(){
            _canvasGroupButtons.interactable = true;

            _canvasGroupButtons.alpha = 0;
            _btnContinue.onClick.RemoveAllListeners();
            _btnContinue.onClick.AddListener(() => { Continue(); });

            _btnLevelSelection.onClick.RemoveAllListeners();
            _btnLevelSelection.onClick.AddListener(() => { LevelSelection(); });

            _btnExit.onClick.RemoveAllListeners();
            _btnExit.onClick.AddListener(() => { Exit(); });

            _btnBackToMenu.onClick.RemoveAllListeners();
            _btnBackToMenu.onClick.AddListener(() => { BackToMenu(); });
        }

        private void Continue(){
            _canvasGroupButtons.interactable = false;
            ScenesManager.Instance.LoadGameScene(true);
            GameEvents.LoadLevel(PrefsManager.LevelSystem.Progress);
            GameEvents.GameStart();
        }

        private void Exit(){
            _canvasGroupButtons.interactable = false;
            Application.Quit();
        }

        private void BackToMenu(){
            _canvasGroupLevelSelection.interactable = false;
            StartCoroutine(DoFade(_canvasGroupLevelSelection, 0f, 0.5f, AnimationCurve.Linear(0f, 0f, 1f, 1f),
                                  () => {
                                      StartCoroutine(DoFade(_canvasGroupButtons, 1f, 0.5f,
                                                            AnimationCurve.Linear(0f, 0f, 1f, 1f),
                                                            () => { _canvasGroupButtons.interactable = true; }));
                                  }));
        }

        private void LevelSelection(){
            _canvasGroupButtons.interactable = false;
            ShowLevelSelection();
            _levelSelectionPanel.Show();
        }

        private void ShowLevelSelection(){
            _canvasGroupButtons.interactable = false;
            StartCoroutine(DoFade(_canvasGroupButtons, 0f, 0.5f, AnimationCurve.Linear(0f, 0f, 1f, 1f),
                                  () => {
                                      StartCoroutine(DoFade(_canvasGroupLevelSelection, 1f, 0.5f,
                                                            AnimationCurve.Linear(0f, 0f, 1f, 1f),
                                                            () => { _canvasGroupLevelSelection.interactable = true; }));
                                  }));
        }

        public override void OnSubscribeEvents(){
        }

        public override void OnUnsubscribeEvents(){
        }


        public override void OnShow(bool withTransition = true){
            if (withTransition) TransitionHelper.Instance.MakeTransition(1f, Input.mousePosition, () => { });
            TurnScreen(true);
        }

        public override void OnHide(bool withTransition = true){
            Action Callback = () => { };
            if (withTransition) TransitionHelper.Instance.MakeTransition(1f, Input.mousePosition, Callback, false);
            TurnScreen(false);
        }


        private void TurnScreen(bool state){
            _canvas.enabled       = state;
            _raycaster.enabled    = state;
            _canvasScaler.enabled = state;
            _contentRoot.SetActive(state);
            if (state == true){
                _canvasGroupButtons.interactable = true;
                StartCoroutine(DoFade(_canvasGroupButtons, 1f, 0.5f, AnimationCurve.Linear(0f, 0f, 1f, 1f), () => { }));
            } else{
                StartCoroutine(DoFade(_canvasGroupButtons, 0f, 0.5f, AnimationCurve.Linear(0f, 0f, 1f, 1f), () => { }));
            }
        }

        private IEnumerator DoFade(CanvasGroup image, float endValue, float time, AnimationCurve curve,
                                   Action      Callback){
            float timer = 0;
            while (timer < time){
                timer += Time.deltaTime;
                if (endValue > 0)
                    image.alpha = curve.Evaluate(timer / time) * endValue;
                else{
                    image.alpha = curve.Evaluate(1f - timer / time);
                }

                yield return null;
            }

            Callback();
        }
    }
}