using System.Collections.Generic;
using UnityEngine;

namespace Korovetskyi_Lab1{
    public class CoinsController : Controller{
        public override void OnSubscribeEvents(){
            GameplayEvents.CoinCollected += AddCoin;
            GameEvents.LevelPassed       += OnLevelPassed;
        }

        public override void OnUnsubscribeEvents(){
            GameplayEvents.CoinCollected -= AddCoin;
            GameEvents.LevelPassed       -= OnLevelPassed;
        }

        private void AddCoin(){
            PrefsManager.Currency.AddCoins(1);
        }

        private void OnLevelPassed(){
            PrefsManager.Currency.AddCoins(10);
        }
    }
}