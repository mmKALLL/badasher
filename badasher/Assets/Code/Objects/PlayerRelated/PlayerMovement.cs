using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	// Contains methods (for FixedUpdate) for dashing, jumping, airdashing and running



	public void PlayerDashUpdate (Player player, Vector3 startingPos, Rigidbody2D playerRig, out float dashDistanceRemaining){
		
	}


	public void PlayerBoostUpdate (Player player, Vector3 startingPos, Rigidbody2D playerRig, out float dashDistanceRemaining){
		
	}
		

	public void PlayerJump (Player player, Rigidbody2D playerRig, Vector3 dir) { // dir should include direction and power, calculated by
		playerRig.AddForce (dir * PlayerConstants.JUMP_POWER);
	}

	// calculate jump modifier in calculationLibrary
	public void PlayerJumpDash (Player player, Rigidbody2D playerRig, Vector3 dir, float jumpModifier, out float dashDistanceRemaining){
		if (player.dashDistanceRemaining <= 0) {
			EndDash (player);
			PlayerJump(player, playerRig, dir*jumpModifier);
			dashDistanceRemaining = 0;
			return;
		}
		float moveAmount = PlayerConstants.DASH_SPEED * Time.fixedDeltaTime;
		if (moveAmount > player.dashDistanceRemaining) {
			moveAmount = player.dashDistanceRemaining;
		}
		playerRig.MovePosition(transform.position + dir * (moveAmount));
		dashDistanceRemaining = player.dashDistanceRemaining-moveAmount;
	}

	public void PlayerFall(Player player){


	}

	public void PlayerRun(Player player){
	
	}



	public void EndDash(Player player){
		player.SetDashState() = Player.DashState.none;
	}

	public void Land(Player player){
		player.SetAirState() = Player.AirState.ground;
	}
}
