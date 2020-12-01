using UnityEngine;

namespace Korovetskyi_Lab1{
    public class TimeController : Controller{
        public float slowdownFactor = 0.05f;
        public float slowdownLength = 2f;

        public override void OnSubscribeEvents(){
            GameplayEvents.Damaged += DoSlowmotion;
        }

        public override void OnUnsubscribeEvents(){
            GameplayEvents.Damaged -= DoSlowmotion;
        }

        public override void OnTick(float dt){
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale =  Mathf.Clamp(Time.timeScale, 0f, 1f);
        }

        public void DoSlowmotion(int damageAmount){
            Time.timeScale      = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }
    }
}