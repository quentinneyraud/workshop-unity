using UnityEngine;
using System.Collections;

namespace Workshop {
	public class WorldManager
	{
		private static WorldManager instance;
		private int world = 0;
		private CameraController cameraController;
		protected AudioSource ambiantAudioNormalWorld;
		protected AudioSource ambiantAudioUpsideDownWorld;

		public static WorldManager getInstance() {
			if (instance == null) {
				instance = new WorldManager();
			}

			return instance;
		}

		public WorldManager () {
			cameraController = Camera.main.GetComponent<CameraController> ();
			ambiantAudioNormalWorld = Camera.main.GetComponents<AudioSource> ()[0];
			ambiantAudioUpsideDownWorld = Camera.main.GetComponents<AudioSource> ()[1];
		}

		public void ToggleWorld () {
			world = (isNormalWorld()) ? 1 : 0;
			cameraController.ToggleUpSideDownEffect ();

			if (isUpsideDownWorld ()) {
				ambiantAudioNormalWorld.Pause ();
				ambiantAudioUpsideDownWorld.Play ();
			} else {
				ambiantAudioNormalWorld.Play ();
				ambiantAudioUpsideDownWorld.Pause ();
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

