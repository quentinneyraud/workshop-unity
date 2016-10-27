using UnityEngine;
using System.Collections;

abstract public class Application : MonoBehaviour
{

	protected Camera camera;
	protected AudioSource ambiantAudio;
	protected GameObject player;

	// Use this for initialization
	protected void Start ()
	{
		camera = Camera.main;
		ambiantAudio = camera.GetComponent<AudioSource> ();
		player = GameObject.FindGameObjectWithTag ("player");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

