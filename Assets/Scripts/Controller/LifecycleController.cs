using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Korovetskyi_Lab1 {
	public sealed class LifecycleController : MonoBehaviour {
		[SerializeField]                private List<Controller> _levelControllersList = null;
		[SerializeField]                private bool             _setTimeScale         = false;
		[SerializeField, Range(0f, 2f)] private float            _timeScale            = 1f;
		private                                 void             Awake() { }
		private                                 void             Start() { }

		private void OnEnable() {
			SetEvents(_levelControllersList);
			Initialization(_levelControllersList);
		}

		private void OnDisable()   { SetEvents(_levelControllersList, subsribe: false); }
		private void FixedUpdate() { UpdateControllers(UpdateType.FixedUpdate, _levelControllersList); }

		private void Update() {
			if (_setTimeScale) {
				Time.timeScale = _timeScale;
			}
			UpdateControllers(UpdateType.Update, _levelControllersList);
		}

		private void LateUpdate() { UpdateControllers(UpdateType.LateUpdate, _levelControllersList); }

		private void UpdateControllers(UpdateType type, List<Controller> list) {
			foreach (Controller controller in list) {
				if (!controller.disabled)
					switch (type) {
						case UpdateType.FixedUpdate:
							controller.FixedTick(Time.fixedDeltaTime);
							break;
						case UpdateType.Update:
							controller.Tick(Time.deltaTime);
							break;
						case UpdateType.LateUpdate:
							controller.LateTick(Time.deltaTime);
							break;
					}
			}
		}

		private void SetEvents(List<Controller> list, bool subsribe = true) {
			foreach (Controller controller in list) {
				if (!controller.disabled)
					if (subsribe) controller.SubscribeEvents();
					else controller.UnsubscribeEvents();
			}
		}

		private void Initialization(List<Controller> list) {
			foreach (Controller controller in list) {
				if (!controller.disabled) controller.Init();
			}
		}

		private void UpdateList() {
			_levelControllersList.Clear();
			for (int i = 0; i < transform.GetChild(0).childCount; i++) {
				_levelControllersList.Add(transform.GetChild(0).GetChild(i).GetComponent<Controller>());
			}

		}

		private enum UpdateType { FixedUpdate, Update, LateUpdate }

	}

}