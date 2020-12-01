using UnityEngine;
using UnityEngine.UI;

namespace Korovetskyi_Lab1{
    public class ResultScreen : UIScreen{
        [SerializeField] private Button _btnRestart = null;

        [Header("Toggle")] [SerializeField] private GraphicRaycaster _raycaster = null;

        [SerializeField] private Canvas       _canvas       = null;
        [SerializeField] private CanvasScaler _canvasScaler = null;
        [SerializeField] private GameObject   _contentRoot  = null;

        public override void OnInit(){
            _btnRestart.onClick.RemoveAllListeners();
            _btnRestart.onClick.AddListener(() => {
                                                _btnRestart.interactable = false;
                                                GameEvents.LoadLevel(PrefsManager.LevelSystem.Progress);
                                                GameEvents.GameStart();
                                                OnHide();
                                            });
        }

        public override void OnSubscribeEvents(){
        }

        public override void OnUnsubscribeEvents(){
        }

        public override void OnShow(bool withTransition = true){
            TurnScreen(true);
            _btnRestart.interactable = true;
            
        }

        public override void OnHide(bool withTransition = true){
            TurnScreen(false);
        }

        private void TurnScreen(bool state){
            _canvas.enabled       = state;
            _raycaster.enabled    = state;
            _canvasScaler.enabled = state;
            if (state == true){
                _contentRoot.SetActive(true);
            } else{
                _contentRoot.SetActive(false);
            }
        }
    }
}