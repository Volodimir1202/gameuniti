using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Korovetskyi_Lab1{
    public class LevelSelectionPanel : UIPanel{
        [SerializeField] private List<Button> _levels;

        public override void OnShow(){
            for (int i = 0; i < _levels.Count; i++){
                _levels[i].interactable = false;
                _levels[i].onClick.RemoveAllListeners();
            }
            for (int i = 0; i < PrefsManager.LevelSystem.Progress; i++){
                _levels[i].interactable = true;
                _levels[i].onClick.AddListener(() => {
                                                   GameEvents.LoadLevel(i);
                                                   GameEvents.GameStart();
                                               });
            }
        }
    }
}