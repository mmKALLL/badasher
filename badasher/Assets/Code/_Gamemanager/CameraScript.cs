using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	Player player;
	Vector3 offset;
	float camHorizontalExtent;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent <Player> ();
		offset = new Vector3 (0, 0, -10);
		camHorizontalExtent = this.GetComponent<Camera> ().orthographicSize;
		this.GetComponent<AudioSource> ().Play ();

	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 attemptedPosition = player.transform.position + offset;
		attemptedPosition.y = Mathf.Clamp (attemptedPosition.y, -25+camHorizontalExtent, 25-15-camHorizontalExtent);
		transform.position = attemptedPosition;
	}
}
