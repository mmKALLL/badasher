using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CalculationLibrary {
	// Holds methods for jump vs dash distance, dash slowdown vs dash distance

	public static float CalculateDashSlowdown (float dashDistanceRemaining){
		//float slowdown;
		float difference = PlayerConstants.DASH_DISTANCE - dashDistanceRemaining;
		if (difference <= PlayerConstants.DASH_SLOWDOWN_PERFECT_PERCENTAGE * PlayerConstants.DASH_DISTANCE) {
			return 0; // no slowdown
		} else if (difference >= PlayerConstants.DASH_SLOWDOWN_END_PERCENTEGE * PlayerConstants.DASH_DISTANCE) {
			return difference*PlayerConstants.DASH_SLOWDOWN_END_MODIFIER;
		} else {
			return difference;
		}
		// slowdown is normalized difference?
	}

	// direction should be normalized
	public static Vector3 CalculateDashJumpDir (float dashDistanceRemaining){
		float distancePercentageLeft = 1-(PlayerConstants.DASH_DISTANCE - dashDistanceRemaining) / PlayerConstants.DASH_DISTANCE;
		distancePercentageLeft = Mathf.Max (Mathf.Min (1, distancePercentageLeft), 0);
		Debug.Log ("percentage" + distancePercentageLeft);
		float dirY = distancePercentageLeft *(-1 + 2*PlayerConstants.DIRECTION_PERCENTAGE_MAX) + (1-PlayerConstants.DIRECTION_PERCENTAGE_MAX);
		float dirX = 1 - dirY;
		Debug.Log(dirY+" + "+dirX);
		return new Vector3 (dirX, dirY, 0).normalized;
	}

	// dir should be received from above function
	public static float CalculateDashJumpPower (float dashDistanceRemaining, bool boostPower){
		float distancePercentageLeft = 1-(PlayerConstants.DASH_DISTANCE - dashDistanceRemaining) / PlayerConstants.DASH_DISTANCE;
		distancePercentageLeft = Mathf.Max (Mathf.Min (1, distancePercentageLeft), 0);
		float power = distancePercentageLeft * 0.7f + 1.1f;
		Debug.Log ("JumpPower " + power);
		return power;
	}
}
