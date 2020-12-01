using System;
using UnityEngine;

namespace Korovetskyi_Lab1{
    public class PrefsManager{
        public static class LevelSystem{
            private const string LEVEL_PROGRESS = "LEVEL_PROGRESS";

            public static int Progress{
                get{
                    return PlayerPrefs.GetInt(LEVEL_PROGRESS, 0);
                }
                set{
                    PlayerPrefs.SetInt(LEVEL_PROGRESS, value);
                }
            }
        }

        public static class Currency{
            private const string COINS = "COINS";

            public static int CoinsAmount{
                get{
                    return PlayerPrefs.GetInt(COINS, 0);
                }
            }

            public static void AddCoins(int amount){
                if (amount >= 1)
                    PlayerPrefs.SetInt(COINS, PlayerPrefs.GetInt(COINS, 0) + amount);
            }
        }

        public static class Player{
            private const string LIFES = "LIFES";

            public static int Lifes{
                get{
                    return PlayerPrefs.GetInt(LIFES, 0);
                }
            }

            public static void AddLife(){
                if (PlayerPrefs.GetInt(LIFES, 0) > 3) return;
                PlayerPrefs.SetInt(LIFES, PlayerPrefs.GetInt(LIFES, 0) + 1);
            }

            public static void RemoveLifes(int amount){
                if (PlayerPrefs.GetInt(LIFES, 0) - amount < 1) return;
                PlayerPrefs.SetInt(LIFES, PlayerPrefs.GetInt(LIFES, 0) - amount);
            }

            public static void ReseLifes(){
                PlayerPrefs.SetInt(LIFES, 3);
            }
        }
    }
}