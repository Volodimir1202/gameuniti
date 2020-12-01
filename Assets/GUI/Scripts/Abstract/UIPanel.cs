using UnityEngine;

namespace Korovetskyi_Lab1 {
	public class UIPanel : MonoBehaviour {
		[SerializeField] protected bool      _active = false;
		public                     void      Init() { OnInit(); }

		public void Show(bool instant = false) {
			if (!_active) {
				_active = true;
				OnShow();
			}
		}

		public void Hide(bool instant = false) {
			if (_active) {
				_active = false;
				OnHide();
			}
		}

		public void Tick(float dt) { OnTick(dt); }
		

		public virtual void      OnInit()         { }
		public virtual void      OnShow()         { }
		public virtual void      OnHide()         { }
		public virtual void      OnTick(float dt) { }
		public         bool      active           { get { return _active; } set { _active = value; } }
	}
}