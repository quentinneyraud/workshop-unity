using UnityEngine;
using System.Collections;

public class Tutoriel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (HideTutoriel ());
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator HideTutoriel () {
		yield return new WaitForSeconds (4);

		Destroy (this.gameObject);
	}

}
