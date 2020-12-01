using UnityEngine;

namespace Korovetskyi_Lab1 {

	public class GUIController : MonoBehaviour {
		[SerializeField] private UIScreen _menuScreenPrefab   = null;
		[SerializeField] private UIScreen _gameScreenPrefab   = null;
		[SerializeField] private UIScreen _resultScreenPrefab = null;

		private UIScreen _menuScreen   = null;
		private UIScreen _gameScreen   = null;
		private UIScreen _resultScreen = null;

		private UIScreen _activeScreen = null;

		private void Start() {
			LoadScreen(ref _menuScreen, _menuScreenPrefab);
			ShowScreen(_menuScreen);
		}

		private void LoadScreen(ref UIScreen screenToLoad, UIScreen prefab) {
			if (screenToLoad != null) return;

			screenToLoad = Instantiate(prefab).GetComponent<UIScreen>();
			screenToLoad.Init();
			screenToLoad.transform.SetParent(transform);
			screenToLoad.SubscribeEvents();
		}

		private void UnloadScreen(ref UIScreen screenToUnload) {
			if (screenToUnload == null) return;

			screenToUnload.UnsubscribeEvents();
			Destroy(screenToUnload.gameObject);
			screenToUnload = null;
		}

		private void OnEnable() {
			GameEvents.LoadLevel         += OnLoadLevel;
			GameEvents.EndGameSession    += OnGameSesstionEnd;
			GameEvents.GameRestart       += OnGameRestart;
			GameEvents.GameOver          += OnLoose;
		}

		private void OnDisable() {
			GameEvents.LoadLevel         -= OnLoadLevel;
			GameEvents.EndGameSession    -= OnGameSesstionEnd;
			GameEvents.GameRestart       -= OnGameRestart;
			GameEvents.GameOver          -= OnLoose;
		}

		private void OnLoadLevel(int lvIndex) {
			LoadScreen(ref _gameScreen, _gameScreenPrefab);
			LoadScreen(ref _resultScreen, _resultScreenPrefab);
			UnloadScreen(ref _menuScreen);
			
			ShowScreen(_gameScreen);

		}

		private void OnGameSesstionEnd() {
			UnloadScreen(ref _resultScreen);
			UnloadScreen(ref _gameScreen);
			LoadScreen(ref _menuScreen, _menuScreenPrefab);
			ShowScreen(_menuScreen);
		}

		private void OnGameRestart() {
			HideScreen(_activeScreen);
			ShowScreen(_gameScreen);
		}

		private void OnLoose() {
			if (_activeScreen == _resultScreen) return;
			HideScreen(_activeScreen, false);
			ShowScreen(_resultScreen, false);
		}

		private void OnWin(int wave) {
			if (_activeScreen == _resultScreen) return;
			HideScreen(_activeScreen, false);
			ShowScreen(_resultScreen, false);
		}

		private void WaveStarted(int enemies) {
			if (_activeScreen == _gameScreen || GameEvents.gameOver) return;
			HideScreen(_activeScreen, false);
			ShowScreen(_gameScreen, false);
		}

		private void ShowScreen(UIScreen screenToShow, bool withTransition = true) {
			if (screenToShow == null) {
				return;
			}

			screenToShow.Show(withTransition);
			_activeScreen = screenToShow;
		}

		private void HideScreen(UIScreen screenToHide, bool withTransition = true) {
			if (screenToHide == null) {
				return;
			}

			if (screenToHide != null) {
				screenToHide.Hide(withTransition);
			}
		}


		private void LateUpdate() { UpdateScreen(_activeScreen); }

		private void UpdateScreen(UIScreen activeScreen) {
			if (_activeScreen != null) activeScreen.Tick(Time.deltaTime);
		}

	}
}