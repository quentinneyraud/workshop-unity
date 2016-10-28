using UnityEngine;
using System.Collections;
using Workshop;

abstract public class Application : MonoBehaviour
{

	new protected Camera camera;
	protected GameObject player;
	protected WorldManager worldManager;

	// Use this for initialization
	protected virtual void Start ()
	{
		camera = Camera.main;
		player = GameObject.FindGameObjectWithTag ("player");
		worldManager = WorldManager.getInstance();
	}

	protected IEnumerator OnGameEnd () {
		GameObject.FindGameObjectWithTag ("Endgame").GetComponent<Canvas>().enabled = true;

		yield return new WaitForSeconds (2);

		UnityEngine.Application.LoadLevel (UnityEngine.Application.loadedLevel);
	}
}

