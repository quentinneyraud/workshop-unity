using UnityEngine;
using System.Collections;
using Workshop;

abstract public class Application : MonoBehaviour
{

	protected Camera camera;
	protected AudioSource ambiantAudio;
	protected GameObject player;
	protected WorldManager worldManager;

	// Use this for initialization
	protected void Start ()
	{
		camera = Camera.main;
		ambiantAudio = camera.GetComponent<AudioSource> ();
		player = GameObject.FindGameObjectWithTag ("player");
		worldManager = WorldManager.getInstance();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

