using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour {
	// Contains methods for determining which collision method to use when colliding to enemy
	// Also contains what to do when colliding with Anything else (ledges etc)

	private Player player;
	private PlayerMovement playerMov;

	public void Awake() {
		player = this.GetComponent<Player> ();
		playerMov = this.GetComponent<PlayerMovement> ();
	}

	public void OnTriggerEnter2D (Collider2D other) {
		if (player.GetLiveState == Player.LiveState.dead) {
			
		} else if (other.CompareTag ("Enemy")) {
			_Enemy enemyScript = other.GetComponent<_Enemy> ();
			if (enemyScript == null) {
				Debug.Log ("ENEMY HAS NO _ENEMY SCRIPT!");
			}
			if (player.GetDashState == Player.DashState.none) { // doesn't check for boost down the line, assumes that there are no other non-dash stuff in this enum
				enemyScript.OnRunThrough();
			} else if (player.GetAirState == Player.AirState.air) {
				enemyScript.OnAirDashThrough ();
			} else if (player.GetAirState == Player.AirState.ground){
				enemyScript.OnDashThrough ();
			}

		} else if (other.CompareTag ("Ramp")) {
			if ((player.GetAirState == Player.AirState.ground) && (player.GetDashState != Player.DashState.none)) {
				playerMov.PlayerJump (player);
			}
		} else if (other.CompareTag ("Powerup")) {
			// call powerup gain method
		}
	}
}
