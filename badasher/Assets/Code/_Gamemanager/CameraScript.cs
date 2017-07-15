using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	Player player;
	Vector3 offset;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent <Player> ();
		offset = new Vector3 (0, 0, -10);

	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
