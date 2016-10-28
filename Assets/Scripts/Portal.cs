using UnityEngine;
using System.Collections;

public class Portal : Application {

	public GameObject to;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "player" && to) {
			player.transform.position = to.transform.position;
			worldManager.ToggleWorld ();
		}
	}
}
