using UnityEngine;

namespace Korovetskyi_Lab1 {
	public class CollisionBase : MonoBehaviour {
		protected bool FindTag(GameObject other, string[] tags) {
			foreach (var tag in tags) {
				if (other.CompareTag(tag)) return true;
			}

			return false;
		}
	}
}