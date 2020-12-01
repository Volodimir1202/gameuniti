using UnityEngine;

namespace Korovetskyi_Lab1{
    public sealed class AppController : MonoBehaviour{
        private const int MENU_FRAME_RATE = 30;
        private const int GAME_FRAME_RATE = 60;

        private void Awake(){
            DontDestroyOnLoad(gameObject);
            LoadGame();
            SetFrameRate(MENU_FRAME_RATE);
        }


        private void OnEnable(){
            GameEvents.LoadLevel      += OnLevelLoad;
            GameEvents.EndGameSession += OnGameSessionEnd;
        }

        private void OnDisable(){
            GameEvents.LoadLevel      -= OnLevelLoad;
            GameEvents.EndGameSession -= OnGameSessionEnd;
        }

        private void OnLevelLoad(int lvIndex){
            SetFrameRate(GAME_FRAME_RATE);
        }

        private void OnGameSessionEnd(){
            SetFrameRate(MENU_FRAME_RATE);
        }

        private void LoadGame(){
            ScenesManager.Instance.LoadGameScene();
            ScenesManager.Instance.LoadGuiScene(true);
        }

        private void SetFrameRate(int frameRate){
            #if !UNITY_EDITOR
				Application.targetFrameRate = frameRate;
            #endif
        }
    }
}