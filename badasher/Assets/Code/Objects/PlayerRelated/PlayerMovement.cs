using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	// Contains methods (for FixedUpdate) for dashing, jumping, airdashing and running

	public void PlayerDashUpdate (Player player, Rigidbody2D playerRig, out float dashDistanceRemaining, bool boostPower){
		float moveAmount = DetermineConstantDashModifier(boostPower) * Time.fixedDeltaTime;
		if (player.dashDistanceRemaining <= 0) {
			player.PlayerDashEnd ();
			Debug.Log ("Dash end");
			dashDistanceRemaining = 0;
			if (player.GetAirState () == Player.AirState.air) {
				playerRig.velocity = Vector3.right*moveAmount/Time.fixedDeltaTime/PlayerConstants.JUMP_DASH_AFTER_MOMENTUM_MODIFIER;
			}
			return;
		}
		if (moveAmount > player.dashDistanceRemaining) {
			moveAmount = player.dashDistanceRemaining;
		}
		playerRig.MovePosition (transform.position + Vector3.right * moveAmount);
		dashDistanceRemaining = player.dashDistanceRemaining-moveAmount;
	}
		

	public void PlayerContinue (Player player, Rigidbody2D playerRig, Vector3 vec) { // vec should include direction and power
		playerRig.velocity = vec;
	}

	// calculate jump modifier in calculationLibrary
	public void PlayerJumpDash (Player player, Rigidbody2D playerRig, Vector3 dir, float jumpPower, out float dashDistanceRemaining, bool boostPower){
		float moveAmount = DetermineConstantDashModifier(boostPower) * jumpPower * Time.fixedDeltaTime;
		playerRig.velocity = dir*moveAmount/Time.fixedDeltaTime/1.6f;
		if (player.dashDistanceRemaining <= 0) {
			player.PlayerDashEnd ();
			playerRig.velocity = dir*moveAmount/Time.fixedDeltaTime/PlayerConstants.JUMP_DASH_AFTER_MOMENTUM_MODIFIER;
			playerRig.isKinematic = false;
			dashDistanceRemaining = 0;
			Debug.Log ("DASHJUMP END");
			return;
		}
		if (moveAmount > player.dashDistanceRemaining) {
			moveAmount = player.dashDistanceRemaining;
		}
		playerRig.MovePosition(transform.position + dir * (moveAmount));
		dashDistanceRemaining = player.dashDistanceRemaining-moveAmount;
	}

	private float DetermineConstantDashModifier(bool boostPower){
		if (boostPower) {
			return PlayerConstants.BOOST_POWER_SPEED;
		} else {
			return PlayerConstants.DASH_SPEED;
		}
	}

	public void PlayerFall (Player player, Rigidbody2D playerRig){
		playerRig.isKinematic = false;
		// nothing, let the gravity do its work.
	}

	public void PlayerRun (Player player, Rigidbody2D playerRig){
		playerRig.MovePosition(transform.position + Vector3.right * PlayerConstants.RUN_SPEED * Time.fixedDeltaTime);
	}
}
