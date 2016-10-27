using UnityEngine;
using System.Collections;

public class CloudBounce : MonoBehaviour {

	void Start () {
		SetCloudFloatingeffect ();
	}

	void Update () {
		
	}

	// Tween cloud position
	void SetCloudFloatingeffect() {
		iTween.MoveTo (this.gameObject, iTween.Hash(
			"x", transform.position.x + 0.8f,
			"y", transform.position.y + 0.15f,
			"time", 6f,
			"easetype", "linear",
			"looptype", "pingPong"
		));
	}
}
