using UnityEngine;
using System.Collections;

namespace Workshop {
	public class WorldManager
	{
		private static WorldManager instance;
		private int world = 0;
		private CameraController cameraController;
		protected AudioSource ambiantAudio;

		public static WorldManager getInstance() {
			if (instance == null) {
				instance = new WorldManager();
			}

			return instance;
		}

		public WorldManager () {
			cameraController = Camera.main.GetComponent<CameraController> ();
			ambiantAudio = Camera.main.GetComponent<AudioSource> ();
		}

		public void ToggleWorld () {
			world = (isNormalWorld()) ? 1 : 0;
			cameraController.ToggleUpSideDownEffect ();

			if (isUpsideDownWorld ()) {
				iTween.AudioTo (ambiantAudio.gameObject, 0, 1, 1.5f);
			} else {
				iTween.AudioTo (ambiantAudio.gameObject, 100, 1, 1.5f);
			}
		}

		public bool isNormalWorld () {
			return world == 0;
		}

		public bool isUpsideDownWorld () {
			return world == 1;
		}
	}
}

