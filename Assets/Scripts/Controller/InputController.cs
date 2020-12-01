using UnityEngine;

namespace Korovetskyi_Lab1{
    public class InputController : Controller{
        [SerializeField] private KeyCode _jump;
        [SerializeField] private KeyCode _fire1;
        [SerializeField] private KeyCode _fire2;
        [SerializeField] private KeyCode _left;
        [SerializeField] private KeyCode _right;
        private                  bool    _interaction = false;

        public override void OnTick(float dt){
            if (!GameEvents.gameStarted ||
                GameEvents.gameOver     ||
                GameEvents.gamePaused) return;
            UpdateInput();
        }

        private void UpdateInput(){
            
            if (Input.GetKeyDown(_fire1)){
                GameplayEvents.MeleeAttack();
            } 

            if (Input.GetKeyDown(_fire2)){
                GameplayEvents.RangeAttack();
            } 

            if (Input.GetKey(_left)){
                GameplayEvents.Move(Vector2.left);
                _interaction = true;
            } else{
                if (Input.GetKey(_right)){
                    GameplayEvents.Move(Vector2.right);
                    _interaction = true;
                } else{
                    _interaction = false;
                }
            }

            if (Input.GetKeyDown(_jump)){
                GameplayEvents.Jump();
            } 

            if (_interaction == false){
                GameplayEvents.Stop();
            }
            GameplayEvents.FocusOnPoint(Input.mousePosition);
        }
    }
}