using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Korovetskyi_Lab1{
    public class FadeTransition : TransitionHelper{
        [SerializeField] private Image          _image           = null;
        [SerializeField] private AnimationCurve _transitionCurve = null;


        public override void MakeTransition(float time, Vector2 pos, Action TransitionCallback, bool show = true){
            if (_image == null){
                TransitionCallback();
                return;
            }

            if (show){
                TransitionAnimation(_image, time, true, TransitionCallback);
            } else{
                TransitionAnimation(_image, time, false, TransitionCallback);
            }
        }

        private void TransitionAnimation(Image image, float time, bool show, Action Callback){
            Color c = image.color;
            if (show){
                c.a         = 1;
                image.color = c;
                StartCoroutine(DoFade(image, 0, time, _transitionCurve, () => { Callback(); }));
            } else{
                c.a         = 0;
                image.color = c;
                StartCoroutine(DoFade(image, 1, time, _transitionCurve, () => { Callback(); }));
            }
        }

        private IEnumerator DoFade(Image image, float endValue, float time, AnimationCurve curve, Action Callback){
            float timer = 0;
            while (timer < time){
                timer += Time.deltaTime;
                Color c = image.color;
                if(endValue > 0)
                    c.a = curve.Evaluate(timer / time) * endValue;
                else{
                    c.a = curve.Evaluate(1f -timer / time);
                }
                image.color = c;
                yield return null;
            }

            Callback();
        }
    }
}