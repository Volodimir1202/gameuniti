using System.Collections.Generic;
using UnityEngine;

namespace Korovetskyi_Lab1{
    public class EnvController : Controller{
        [SerializeField] private List<Material> _skyboxes      = null;
        [SerializeField] private float          _rotationSpeed = 2f;

        private Material _current = null;

        public override void OnSubscribeEvents(){
            SetRandomSkybox(0);
            GameEvents.LoadLevel += SetRandomSkybox;
        }

        public override void OnUnsubscribeEvents(){
            GameEvents.LoadLevel -= SetRandomSkybox;
        }

        public override void OnTick(float dt){
            if (_current != null){
                _current.SetFloat("_Rotation", _current.GetFloat("_Rotation") + _rotationSpeed * dt);
                if (_current.GetFloat("_Rotation") > 360){
                    _current.SetFloat("_Rotation", 0);
                }
            }
        }

        private void SetRandomSkybox(int lvIndex){
            RenderSettings.skybox = _current = _skyboxes[Random.Range(0, _skyboxes.Count)];
        }
    }
}