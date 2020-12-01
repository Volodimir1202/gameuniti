using System;
using UnityEngine;

namespace Korovetskyi_Lab1{
    public static class GameEvents{
        public static bool levelLoaded = false;

        public static bool gameStarted = false;
        public static bool gamePaused  = false;
        public static bool gameOver    = true;

        public static Action GameStart = delegate{
                                             gameOver    = false;
                                             gameStarted = true;
                                         };


        public static Action GameRestart = delegate{
                                               gameOver    = false;
                                               gameStarted = false;
                                               levelLoaded = false;
                                           };

        public static Action GameOver = delegate{
                                            gameOver    = true;
                                            gameStarted = false;
                                        };

        public static Action GamePaused   = delegate{ gamePaused = true; };
        public static Action GameUnpaused = delegate{ gamePaused = false; };

        public static Action<int> LoadLevel = delegate{
                                                  levelLoaded = true;
                                                  gameOver    = false;
                                                  gameStarted = false;
                                              };


        public static Action EndGameSession = delegate{
                                                  levelLoaded = false;
                                                  gameOver    = true;
                                                  gameStarted = false;
                                              };


        public static Action<Level> LevelLoaded = delegate{ };
        public static Action        LevelPassed = delegate{ gameStarted = false; };
    }
}