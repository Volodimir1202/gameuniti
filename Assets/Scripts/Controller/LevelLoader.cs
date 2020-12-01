using System.Collections.Generic;
using UnityEngine;

namespace Korovetskyi_Lab1{
    public class LevelLoader : Controller{
        [SerializeField] public Transform   _container = null;
        [SerializeField] public List<Level> _catalog   = null;
        private                 int         _levelIndex;
        private                 Level       _currentLevel;


        public override void OnInit(){
        }

        public override void OnSubscribeEvents(){
            GameEvents.LoadLevel      += LoadLevel;
            GameEvents.GameRestart    += RestartLevel;
            GameEvents.EndGameSession += OnEndSession;
            GameEvents.LevelPassed    += OnLevelPassed;
        }

        public override void OnUnsubscribeEvents(){
            GameEvents.LoadLevel      -= LoadLevel;
            GameEvents.GameRestart    -= RestartLevel;
            GameEvents.EndGameSession -= OnEndSession;
            GameEvents.LevelPassed    -= OnLevelPassed;
        }

        public override void OnTick(float dt){
            if (_currentLevel != null){
                _currentLevel.OnTick(dt);
            }
        }

        private void OnLevelPassed(){
            _levelIndex += 1;
            if (_levelIndex >= _catalog.Count){
                _levelIndex = 0;
            }

            SaveProgress(_levelIndex);
            GameEvents.LoadLevel(_levelIndex);
            GameEvents.GameStart();
        }


        private void OnEndSession(){
            CleanCntainer();
        }

        private void SaveProgress(int progress){
            PrefsManager.LevelSystem.Progress = progress;
        }

        private void LoadLevel(int lvIndex){
            CleanCntainer();
            _levelIndex = lvIndex;
            CreateAndSetupLevel(_catalog, _levelIndex);
        }

        private void RestartLevel(){
            CleanCntainer();
            CreateAndSetupLevel(_catalog, _levelIndex);
        }

        private void CreateAndSetupLevel(List<Level> list, int index){
            _currentLevel = Instantiate(list[index], Vector3.zero, Quaternion.identity,
                                        _container);

            _currentLevel.Init();

            GameEvents.LevelLoaded(_currentLevel);
        }

        private void CleanCntainer(){
            if (_container.childCount > 0){
                for (int i = _container.childCount - 1; i >= 0; i--){
                    Destroy(_container.GetChild(i).gameObject);
                }
            }
        }
    }
}