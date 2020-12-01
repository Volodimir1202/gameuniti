using System;
using UnityEngine;

namespace Korovetskyi_Lab1{
    public class TransitionHelper : Singleton<TransitionHelper>{
        public virtual void MakeTransition(float time, Vector2 pos, Action TransitionCallback, bool show = true){
        }
    }
}