using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLauncher : MonoBehaviour {

	// TODO
	public GameObject enemy_ground;

	// boots up the stuff
	void Start () {
		// TODO
	}

	public void GenerateStage(){
		// TODO ESA
		GameObject instantiatedObject = Instantiate(enemy_ground, transform.position, Quaternion.identity);
		instantiatedObject.transform.localScale = new Vector3 (1, 1, 1);
	}
}
