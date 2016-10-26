using UnityEngine;
using System.Collections;

public class Blade : MonoBehaviour {

	public int spin_speed = 30;

	// Use this for initialization
	void Start () {
		iTween.RotateBy (this.gameObject, iTween.Hash(
			"z", 1f, 
			"easetype", "linear",
			"looptype", iTween.LoopType.loop,
			"time", 5f
		));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
