using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Korovetskyi_Lab1{
    public class GameScreen : UIScreen{
        [SerializeField] private GameObject      _heartPrefab     = null;
        [SerializeField] private Transform       _heartsContainer = null;
        [SerializeField] private Button          _btnPause        = null;
        [SerializeField] private Button          _btnUnpause      = null;
        [SerializeField] private Button          _btnExit         = null;
        [SerializeField] private Button          _btnMenu         = null;
        [SerializeField] private GameObject      _pnlPause        = null;
        [SerializeField] private TextMeshProUGUI _txtCoinsInfo    = null;


        [Header("Toggle")] [SerializeField] private GraphicRaycaster _raycaster = null;

        [SerializeField] private Canvas       _canvas       = null;
        [SerializeField] private CanvasScaler _canvasScaler = null;
        [SerializeField] private GameObject   _contentRoot  = null;

        private List<Image> _hearts = new List<Image>();

        public override void OnInit(){
            _btnPause.onClick.RemoveAllListeners();
            _btnPause.onClick.AddListener(() => { Pause(); });

            _btnUnpause.onClick.RemoveAllListeners();
            _btnUnpause.onClick.AddListener(() => { Unpause(); });

            _btnExit.onClick.RemoveAllListeners();
            _btnExit.onClick.AddListener(() => { Application.Quit(); });

            _btnMenu.onClick.RemoveAllListeners();
            _btnMenu.onClick.AddListener(() => {
                                             Unpause();
                                             ScenesManager.Instance.LoadGuiScene(true);
                                             GameEvents.EndGameSession();
                                         });
            UpdateLifesInfo();
        }

        public override void OnShow(bool withTransition = true){
            TurnScreen(true);
            // ScenesManager.Instance.LoadGameScene(true);
            UpdateCoinsInfo();
            if (withTransition) TransitionHelper.Instance.MakeTransition(1f, Input.mousePosition, () => { });
        }

        private void Pause(){
            _btnPause.interactable = false;
            _btnPause.gameObject.SetActive(false);

            _btnUnpause.interactable = true;
            _btnUnpause.gameObject.SetActive(true);
            GameEvents.GamePaused();
            _pnlPause.SetActive(true);
        }

        private void Unpause(){
            _btnUnpause.interactable = false;
            _btnUnpause.gameObject.SetActive(false);

            _btnPause.interactable = true;
            _btnPause.gameObject.SetActive(true);
            GameEvents.GameUnpaused();
            _pnlPause.SetActive(false);
        }

        public override void OnHide(bool withTransition = true){
            TurnScreen(false);
        }

        public override void OnSubscribeEvents(){
            GameplayEvents.CoinCollected  += UpdateCoinsInfo;
            GameplayEvents.HeartCollected += AddHeart;
            GameplayEvents.Damaged        += Damaged;
        }

        public override void OnUnsubscribeEvents(){
            GameplayEvents.CoinCollected  -= UpdateCoinsInfo;
            GameplayEvents.HeartCollected -= AddHeart;
            GameplayEvents.Damaged        -= Damaged;
        }


        private void UpdateCoinsInfo(){
            _txtCoinsInfo.text = $"{PrefsManager.Currency.CoinsAmount}";
        }

        private void UpdateLifesInfo(){
            for (int i = 0; i < PrefsManager.Player.Lifes; i++){
                AddHeart();
            }
        }

        private void AddHeart(){
            GameObject heart = Instantiate(_heartPrefab);
            heart.transform.parent = _heartsContainer;
        }

        private void Damaged(int amount){
            if (_heartsContainer.childCount > 1)
                Destroy(_heartsContainer.GetChild(_heartsContainer.childCount - 1).gameObject);
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