using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Korovetskyi_Lab1{
    public class ScenesManager : Singleton<ScenesManager>{
        [SerializeField] private string GUI_SCENE       = "GUI";
        [SerializeField] private string GAME_SCENE      = "Game";
        [SerializeField] private string TUTORIAL_SCENE  = "Tutorial";
        public                   bool   gameSceneLoaded = false;
        public                   bool   guiSceneLoaded  = false;

        private Scene _gameScene;
        private Scene _guiScene;

        private void Awake(){
            DontDestroyOnLoad(this.gameObject);
        }

        private void OnEnable(){
            SceneManager.sceneLoaded   += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnDisable(){
            SceneManager.sceneLoaded   -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        public void UnloadTutorialScene(){
            SceneManager.UnloadSceneAsync(TUTORIAL_SCENE);
        }

        public void LoadGameScene(bool setActive = false, float delay = 0){
            if (!gameSceneLoaded){
                SceneManager.LoadScene(GAME_SCENE, LoadSceneMode.Additive);
                if (setActive){
                    SetActiveWhenLoaded(SceneManager.GetSceneByName(GAME_SCENE));
                }
            } else{
                SetActiveDelayed(_gameScene, delay);
            }
        }

        public void LoadTutorialScene(bool setActive = false){
            SceneManager.LoadScene(TUTORIAL_SCENE, LoadSceneMode.Additive);
            if (setActive){
                SetActiveWhenLoaded(SceneManager.GetSceneByName(TUTORIAL_SCENE));
            }
        }

        public void LoadGuiScene(bool setActive = false, float delay = 0){
            if (!guiSceneLoaded){
                SceneManager.LoadScene(GUI_SCENE, LoadSceneMode.Additive);
                if (setActive){
                    SetActiveWhenLoaded(SceneManager.GetSceneByName(GUI_SCENE));
                }
            } else{
                SetActiveDelayed(_guiScene, delay);
            }
        }

        private void SetActiveDelayed(Scene scene, float delay){
            IEnumerator delayed = null;
            delayed = DelayedCall(delay, () => { SceneManager.SetActiveScene(scene); },
                                  () => { StopCoroutine(delayed); });
            StartCoroutine(delayed);
        }

        private void SetActiveWhenLoaded(Scene scene){
            IEnumerator sawl = null;
            sawl = SetActiveWhenLoaded(scene, () => { SceneManager.SetActiveScene(scene); },
                                       () => { StopCoroutine(sawl); });
            StartCoroutine(sawl);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
            if (scene.name == GUI_SCENE){
                guiSceneLoaded = true;
                _guiScene      = scene;
            }

            if (scene.name == GAME_SCENE){
                gameSceneLoaded = true;
                _gameScene      = scene;
            }
        }

        private void OnSceneUnloaded(Scene scene){
            if (scene.name == GUI_SCENE){
                guiSceneLoaded = false;
            }

            if (scene.name == GAME_SCENE){
                gameSceneLoaded = false;
            }
        }

        private IEnumerator DelayedCall(float time, Action Call, Action Callback){
            yield return new WaitForSeconds(time);
            Call();
            Callback();
        }

        private IEnumerator SetActiveWhenLoaded(Scene scene, Action Call, Action Callback){
            yield return new WaitUntil(() => scene.isLoaded);
            Call();
            Callback();
        }
    }
}