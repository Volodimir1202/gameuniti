using UnityEngine;

namespace Korovetskyi_Lab1{
    public sealed class PlayerController : Controller{
        [SerializeField] private Player          _player      = null;
        [SerializeField] private RangeWeaponBase _rangeWeapon = null;
        [SerializeField] private MeleeWeaponBase _meleeWeapon = null;

        private float _immuneTimer = 0;

        public override void OnInit(){
            _player.weapon.AddMeleeWeapon(_meleeWeapon);
            _player.weapon.AddRangeWeapon(_rangeWeapon);
            PrefsManager.Player.ReseLifes();
        }

        public override void OnSubscribeEvents(){
            GameEvents.LoadLevel += OnLevelLoad;

            GameplayEvents.Move         += ReadMove;
            GameplayEvents.Jump         += Jump;
            GameplayEvents.MeleeAttack  += MeleeAttack;
            GameplayEvents.RangeAttack  += RangeAttack;
            GameplayEvents.FocusOnPoint += FocusOnPoint;
            GameplayEvents.Stop         += Stop;
            GameplayEvents.Damaged      += Damaged;
        }

        public override void OnUnsubscribeEvents(){
            GameEvents.LoadLevel -= OnLevelLoad;

            GameplayEvents.Move         -= ReadMove;
            GameplayEvents.Jump         -= Jump;
            GameplayEvents.MeleeAttack  -= MeleeAttack;
            GameplayEvents.RangeAttack  -= RangeAttack;
            GameplayEvents.FocusOnPoint -= FocusOnPoint;
            GameplayEvents.Stop         -= Stop;
            GameplayEvents.Damaged      -= Damaged;
        }

        public override void OnTick(float dt){
            if (_player.collision.immune){
                _immuneTimer += dt;
            }

            if (_immuneTimer > 3f){
                _player.collision.immune = false;
            }
        }

        private void OnLevelLoad(int index){
            _player.gameObject.SetActive(true);
            PrefsManager.Player.ReseLifes();
            _player.move.SetPosition(new Vector3(0, 2));
        }

        private void ReadMove(Vector2 dir){
            _player.move.OnMove(dir);
        }

        private void Jump(){
            _player.move.OnJump();
        }

        private void Stop(){
            _player.move.Stop();
        }

        private void MeleeAttack(){
            _player.weapon.MeleeAttack();
        }

        private void Damaged(int amount){
            if (GameEvents.gameOver) return;
            GameplayEvents.EnemyKilled(_player.transform.position);
            if (PrefsManager.Player.Lifes - amount < 1){
                GameEvents.GameOver();
                _player.gameObject.SetActive(false);
            }

            _immuneTimer = 0f;
            PrefsManager.Player.RemoveLifes(amount);
            _player.visual.Damaged();
        }

        private void RangeAttack(){
            _player.weapon.RangeAttack();
        }

        private void FocusOnPoint(Vector3 point){
            _player.visual.UpdateFocus(point);
            _player.weapon.UpdateFocus(point);
        }
    }
}