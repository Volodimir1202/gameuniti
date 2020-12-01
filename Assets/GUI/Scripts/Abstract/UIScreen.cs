using System;
using System.Collections;
using UnityEngine;

namespace Korovetskyi_Lab1 {
	public class UIScreen : MonoBehaviour {
		[SerializeField] protected bool     _active = false;
		public                     void     Init() { OnInit(); }

		public void Show(bool withTransition = true) {
			if (!_active) {
				_active = true;
				OnShow(withTransition);
			}
		}

		public void Hide(bool withTransition = true) {
			if (_active) {
				_active = false;
				OnHide(withTransition);
			}
		}

		public void SubscribeEvents()   { OnSubscribeEvents(); }
		public void UnsubscribeEvents() { OnUnsubscribeEvents(); }
		public void Tick(float dt)      { OnTick(dt); }

		public virtual void OnInit()              { }
		public virtual void OnShow(bool withTransition = true)              { }
		public virtual void OnHide(bool withTransition = true)              { }
		public virtual void OnSubscribeEvents()   { }
		public virtual void OnUnsubscribeEvents() { }
		public virtual void OnTick(float dt)      { }
		public         bool active                { get { return _active; } set { _active = value; } }
	}
}