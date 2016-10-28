using UnityEngine;
using System.Collections;

public class Blade : Application {

	protected override void Start () {
		base.Start ();
		// Infinite rotate
		iTween.RotateBy (this.gameObject, iTween.Hash(
			"z", 1f, 
			"easetype", "linear",
			"looptype", iTween.LoopType.loop,
			"time", 5f
		));
	}

	void Update () {
		
	}
}
