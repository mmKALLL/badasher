using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	Player player;
	Vector3 offset;
	float camHorizontalExtent;

	float lastY = 0;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent <Player> ();
		offset = new Vector3 (8.5f, 1.0f, -10);
		camHorizontalExtent = this.GetComponent<Camera> ().orthographicSize;
		this.GetComponent<AudioSource> ().Play ();

	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 attemptedPosition = player.transform.position + offset;
		if (player.GetAirState () == Player.AirState.air) {
			attemptedPosition.y = lastY;
		}
		attemptedPosition.y = Mathf.Clamp (attemptedPosition.y, -25+camHorizontalExtent, 50-15-camHorizontalExtent);
		transform.position = attemptedPosition;
		lastY = attemptedPosition.y;
	}
}
