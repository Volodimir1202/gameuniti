using UnityEngine;

namespace Korovetskyi_Lab1{
    public class CameraController : Controller{
        [SerializeField] private CameraData _camera = null;

        public override void OnInit(){
            _camera.Init();
        }

        public override void OnSubscribeEvents(){
            GameplayEvents.FocusOnPoint += GetFocusPoint;
        }

        public override void OnUnsubscribeEvents(){
            GameplayEvents.FocusOnPoint -= GetFocusPoint;
        }


        public override void OnFixedTick(float fdt){
            _camera.UpdatePosition(fdt);

        }

        private void GetFocusPoint(Vector3 point){
            _camera.SetFocus(point);
        }
    }
}