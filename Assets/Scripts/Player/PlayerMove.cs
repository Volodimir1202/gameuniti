using System;
using UnityEngine;

namespace Korovetskyi_Lab1{
    public class PlayerMove : MonoBehaviour{
        [SerializeField] private Animator    _playerAnimator = null;
        [SerializeField] private Rigidbody2D _rigidbody      = null;

        [SerializeField] private float _jumpForce    = 10f;
        [SerializeField] private float _speedDumping = 10f;
        [SerializeField] private float _speed        = 10f;

        private bool _isGraunded = false;
        private int  _jumpNumber = 0;

        public void OnMove(Vector2 dir){
            _rigidbody.velocity += ((dir * _speed) + Physics2D.gravity) * Time.fixedDeltaTime;
            ClampVelocity(ref _rigidbody, _speed);
            _playerAnimator.SetFloat("Speed", 1f);
            _playerAnimator.SetFloat("SpeedMultiplier", 2);
        }

        public void OnJump(){
            if (_jumpNumber >= 2) return;
            _jumpNumber++;
            //чтобы сила прыжка была всегда одинакова, даже во время падения, нужно ее обнулять перед каждым прыжком
            Vector2 velocityWithoutY = _rigidbody.velocity.normalized;
            velocityWithoutY.y  = 0;
            _rigidbody.velocity = velocityWithoutY;

            Vector2 jump = velocityWithoutY +
                           Vector2.up * _jumpForce * -Physics2D.gravity * Time.fixedDeltaTime;
            _rigidbody.AddForce(jump, ForceMode2D.Impulse);

            _playerAnimator.SetTrigger("Jump");
        }

        public void Stop(){
            //проверка скорости чтобы не делать лишние действия когда игрок стоит
            if (_rigidbody.velocity.magnitude > 0){
                Vector2 velocity = _rigidbody.velocity;
                //постепенно уменьшаем скорость игрока когда нету ввода
                velocity.x          = Mathf.Lerp(velocity.x, 0, Time.fixedDeltaTime * _speedDumping);
                _rigidbody.velocity = velocity;
                _playerAnimator.SetFloat("Speed", 0);
                _playerAnimator.SetFloat("SpeedMultiplier", 2);
            }
        }


        private void OnCollisionStay2D(Collision2D other){
            if (other.gameObject.CompareTag("Ground")){
                _isGraunded = true;
                _jumpNumber = 0;
            }
        }

        private void OnCollisionExit(Collision other){
            if (other.gameObject.CompareTag("Ground")){
                _isGraunded = false;
            }
        }

        private void ClampVelocity(ref Rigidbody2D rigidbody, float maxSpeed){
            //так как у нас есть небольшая инерция от постоянного прибавления вектора скорости, нужно огриничить ее
            var velocity = rigidbody.velocity;
            //убираем с ограничения скорость прыжка или падения
            var f = velocity.y;
            velocity.y         = 0f;
            velocity           = Vector2.ClampMagnitude(velocity, maxSpeed);
            velocity.y         = f;
            rigidbody.velocity = velocity;
        }

        public void SetPosition(Vector2 pos){
            _rigidbody.position    = pos;
        }
    }
}