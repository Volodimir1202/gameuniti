using System;
using UnityEngine;

namespace Korovetskyi_Lab1{
    public class GameplayEvents{
        public static Action          MeleeAttack    = delegate{ };
        public static Action          RangeAttack    = delegate{ };
        public static Action          Jump           = delegate{ };
        public static Action<int>     Damaged        = delegate{ };
        public static Action<Vector2> Move           = delegate{ };
        public static Action          Stop           = delegate{ };
        public static Action<Vector3> FocusOnPoint   = delegate{ };
        public static Action<Vector3> EnemyKilled    = delegate{ };
        public static Action          CoinCollected  = delegate{ };
        public static Action          HeartCollected = delegate{ };
    }
}