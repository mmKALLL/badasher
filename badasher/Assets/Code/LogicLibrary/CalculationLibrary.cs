using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CalculationLibrary {
	// Holds methods for jump vs dash distance, dash slowdown vs dash distance

	public static float CalculateDashSlowdown (float dashDistanceRemaining){
		float slowdown;
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

	public static Vector3 CalculateDashJumpDir (float dashDistanceRemaining){
		return new Vector3();
	}

	public static float CalculateDashJumpPower (float dashDistanceRemaining){
		return 10f;
	}
}
