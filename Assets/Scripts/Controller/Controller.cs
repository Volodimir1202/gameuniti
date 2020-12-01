using UnityEngine;

namespace Korovetskyi_Lab1{
    public class Controller : MonoBehaviour{
        public bool disabled = false;

        public void Init(){
            OnInit();
        }

        public void SubscribeEvents(){
            OnSubscribeEvents();
        }

        public void UnsubscribeEvents(){
            OnUnsubscribeEvents();
        }

        public void Tick(float dt){
            OnTick(dt);
        }

        public void LateTick(float dt){
            OnLateTick(dt);
        }

        public void FixedTick(float fdt){
            OnFixedTick(fdt);
        }

        public virtual void OnInit(){
        }

        public virtual void OnSubscribeEvents(){
        }

        public virtual void OnUnsubscribeEvents(){
        }

        public virtual void OnLateTick(float dt){
        }

        public virtual void OnFixedTick(float fdt){
        }

        public virtual void OnTick(float dt){
        }
    }
}