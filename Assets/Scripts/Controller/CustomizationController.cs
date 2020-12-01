using System.Collections.Generic;
using UnityEngine;

namespace Korovetskyi_Lab1{
    public class CustomizationController : Controller{
        [SerializeField] private PlayerVisual     _playerVisual = null;
        [SerializeField] private List<GameObject> _hats         = null;

        public override void OnSubscribeEvents(){
            SetRandomHat(0);
            GameEvents.LoadLevel += SetRandomHat;
        }

        public override void OnUnsubscribeEvents(){
            GameEvents.LoadLevel -= SetRandomHat;
        }

        private void SetRandomHat(int lvIndex){
            _playerVisual.SetHat(_hats[Random.Range(0,_hats.Count)]);
        }
    }
}